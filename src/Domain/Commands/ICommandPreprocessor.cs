namespace Guidelines.Core.Commands
{
	public interface ICommandPreprocessor
	{
		void PreprocessCommand(object command);
		bool CommandIsEligible(object command);
	}

	public interface ICommandPreprocessor<TCommand>
	{
		void PreprocessCommand(TCommand command);
	}
}
