namespace Guidelines.Core.Commands
{
	public class TypedCommandPreprocessorFacade<TCommand> : ICommandPreprocessor
	{
		private readonly ICommandPreprocessor<TCommand> _preprocessor;

		public TypedCommandPreprocessorFacade(ICommandPreprocessor<TCommand> preprocessor)
		{
			_preprocessor = preprocessor;
		}

		public bool CommandIsEligible(object command)
		{
			return typeof (TCommand).IsAssignableFrom(command.GetType());
		}

		public void PreprocessCommand(object command)
		{
			if (!CommandIsEligible(command))
			{
				return;
			}

			_preprocessor.PreprocessCommand((TCommand)command);
		}
	}
}