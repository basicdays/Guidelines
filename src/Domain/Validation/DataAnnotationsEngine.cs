using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using Guidelines.Domain.Properties;

namespace Guidelines.Domain.Validation
{
	public class DataAnnotationsEngine : IValidationEngine
	{
		public DataAnnotationsEngine(IServiceContainer container, IMapper<IEnumerable<ValidationResult>, IEnumerable<ValidationEngineMessage>> mapper)
		{
			_container = container;
			_mapper = mapper;
		}

		private readonly IServiceContainer _container;
		private readonly IMapper<IEnumerable<ValidationResult>, IEnumerable<ValidationEngineMessage>> _mapper;

		public bool IsValid(object instance)
		{
			ValidationContext context = new ValidationContext(instance, _container, null);
			return TryValidateObjectRecursive(instance, context, null, false);
		}

		public bool IsValid(object instance, out IEnumerable<ValidationEngineMessage> results)
		{
			var context = new ValidationContext(instance, _container, null);
			var dataAnnotationResults = new List<ValidationResult>();
			var valid = TryValidateObjectRecursive(instance, context, dataAnnotationResults, true);

			results = valid
				? new List<ValidationEngineMessage>()
				: _mapper.Map(dataAnnotationResults);
			return valid;
		}

		public void Validate(object instance)
		{
			var context = new ValidationContext(instance, _container, null);
			var dataAnnotationResults = new List<ValidationResult>();
			var valid = TryValidateObjectRecursive(instance, context, dataAnnotationResults, true);

			if (!valid)
			{
				throw new ValidationEngineException(
					_mapper.Map(dataAnnotationResults),
					Resources.Error_ValidationError);
			}
		}

		public void Validate(IEnumerable<object> instances)
		{
			bool valid = true;
			IEnumerable<ValidationEngineMessage> validationErrors = instances.SelectMany(instance =>
			{
				IEnumerable<ValidationEngineMessage> validationResults;
				if (instance != null)
				{
					valid = IsValid(instance, out validationResults) && valid;
				}
				else
				{
					validationResults = new List<ValidationEngineMessage>();
				}
				return validationResults;
			}).ToList();

			if (!valid)
			{
				throw new ValidationEngineException(
					validationErrors,
					Resources.Error_ValidationError);
			}
		}

		//Lazily stolen from blog http://www.tsjensen.com/blog/2011/12/23/Custom+Recursive+Model+Validation+In+NET+Using+Data+Annotations.aspx
		//Then modified to look at the attributes of child objects instead of properties per pauls suggestion.
		/// <summary>
		/// Warps validator try validate method with one that will validate all children on the object that are
		/// taged with the ValidateObjectAttribute attribute.
		/// </summary>
		public static bool TryValidateObjectRecursive<T>(T obj, ValidationContext validationContext, ICollection<ValidationResult> results, bool validateAllProperties)
		{
			bool result = Validator.TryValidateObject(obj, validationContext, results, validateAllProperties);

			IEnumerable<PropertyInfo> properties = obj.GetType().GetProperties().Where(prop =>
				!prop.PropertyType.GetCustomAttributes(typeof(ValidateObjectAttribute), true).IsEmpty()
			);

			foreach (var value in properties.Select(prop => obj.GetPropertyValue(prop.Name)).Where(val => val != null))
			{
				var propertyValidationContext = new ValidationContext(value, validationContext.ServiceContainer, null);
				result = TryValidateObjectRecursive(value, propertyValidationContext, results, validateAllProperties) && result;
			}

			IEnumerable<PropertyInfo> enumerables = obj.GetType().GetProperties().Where(prop => 
				typeof(IEnumerable).IsAssignableFrom(prop.PropertyType)
			);

			foreach (IEnumerable asEnumerable in enumerables
				.Select(prop => obj.GetPropertyValue(prop.Name))
				.Where(val => val != null)
				.Cast<IEnumerable>())
			{
				foreach (object enumObj in asEnumerable)
				{
					if(enumObj.HasAttribute(typeof(ValidateObjectAttribute))) {
						var enumValidationContext = new ValidationContext(enumObj, validationContext.ServiceContainer, null);
						result = TryValidateObjectRecursive(enumObj, enumValidationContext, results, validateAllProperties) && result;
					}
				}
			}

			return result;
		}
	}
}
