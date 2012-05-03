namespace Guidelines.Domain.Commands
{
	public interface ICommandProcessor<in TCommandMessage>
	{
		CommandResult Process(TCommandMessage commandMessage);
	}
}
