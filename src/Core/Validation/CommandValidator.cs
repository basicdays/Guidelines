using Guidelines.Core.Commands;

namespace Guidelines.Core.Validation
{
	public class CommandValidator : ICommandPreprocessor
	{
		private readonly IValidationEngine _validationEngine;

		public CommandValidator(IValidationEngine validationEngine)
		{
			_validationEngine = validationEngine;
		}

		public void PreprocessCommand(object command)
		{
			_validationEngine.Validate(command);
		}

		public bool CommandIsEligible(object command)
		{
			return true;
		}
	}
}
