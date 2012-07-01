using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using Guidelines.Core.Properties;

namespace Guidelines.Core.Validation
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
			var validator = new RecursiveValidator(true, _container);
			return validator.TryValidateObject(instance);
		}

		public bool IsValid(object instance, out IEnumerable<ValidationEngineMessage> results)
		{
			var validator = new RecursiveValidator(true, _container);
			var valid = validator.TryValidateObject(instance);

			results = valid
				? new List<ValidationEngineMessage>()
				: _mapper.Map(validator.Results);
			return valid;
		}

		public void Validate(object instance)
		{
			var validator = new RecursiveValidator(true, _container);
			var valid = validator.TryValidateObject(instance);

			if (!valid)
			{
				throw new ValidationEngineException(
					_mapper.Map(validator.Results));
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
					validationErrors);
			}
		}
	}
}
