namespace Guidelines.Core.Commands
{
	public interface ICommandProcessor<in TCommandMessage>
	{
		CommandResult Process(TCommandMessage commandMessage);
	}
}
