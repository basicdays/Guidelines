using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;

namespace Guidelines.Core.Validation
{
    public class RecursiveValidator
    {
        private readonly IServiceContainer _container;
        private readonly bool _validateAllProperties;

        public RecursiveValidator(bool validateAllProperties, IServiceContainer container)
        {
            _container = container;
            _validateAllProperties = validateAllProperties;

            Results = new List<ValidationResult>();
        }

        public ICollection<ValidationResult> Results { get; set; }
        public bool ValidateAllProperties { get { return _validateAllProperties; } }

        public bool TryValidateObject<T>(T obj) 
            where T : class
        {
            if(obj == null)
            {
                return false;
            }

            //Create context for this validation run
            var validatedObjects = new Dictionary<object, bool>();
            var context = new ValidationContext(obj, _container, null);

            //reset results.
            Results = new List<ValidationResult>();

            //validate
            return TryValidateObject(obj, context, validatedObjects);
        }

        /// <summary>
        /// Wraps validator try validate method with one that will validate all children on the object that are
        /// in the same assembly as the one being validated.
        /// </summary>
        private bool TryValidateObject<T>(T obj, ValidationContext validationContext, Dictionary<object, bool> validatedObjects)
        {
            //Track all validated objects to prevent self referencing trees from crashing.
            //Return the result of this object being validated if it has been done.
            if (validatedObjects.ContainsKey(obj))
            {
                return validatedObjects[obj];
            }

            bool result = Validator.TryValidateObject(obj, validationContext, Results, ValidateAllProperties);

            //store the result of validating this object.
            validatedObjects.Add(obj, result);

			//Get all child properties that have the validate attribute.
			//review: use an exclusion attribute instead of opt in attribute
			IEnumerable<PropertyInfo> properties = obj.GetType().GetProperties().Where(prop =>
				!prop.PropertyType.GetCustomAttributes(typeof(ValidateObjectAttribute), true).IsEmpty()
			);

            //Validate each child.
            foreach (var value in properties
                .Select(prop => obj.GetPropertyValue(prop.Name))
                .Where(val => val != null))
            {
                var propertyValidationContext = new ValidationContext(value, validationContext.ServiceContainer, null);
                result = TryValidateObject(value, propertyValidationContext, validatedObjects) && result;
            }

            //Get all enumerable types on the object.
            IEnumerable<PropertyInfo> enumerables = obj.GetType().GetProperties()
                .Where(prop => typeof(IEnumerable).IsAssignableFrom(prop.PropertyType));

			//Traverse the enumerable and validate any that have validate attribute.
            foreach (IEnumerable asEnumerable in enumerables
                .Select(prop => obj.GetPropertyValue(prop.Name))
                .Where(val => val != null)
                .Cast<IEnumerable>())
            {
                foreach (object enumObj in asEnumerable)
                {
					if (enumObj.HasAttribute(typeof(ValidateObjectAttribute)))
                    {
                        var enumValidationContext = new ValidationContext(enumObj, validationContext.ServiceContainer, null);
                        result = TryValidateObject(enumObj, enumValidationContext, validatedObjects) && result;
                    }
                }
            }

            return result;
        }
    }
}