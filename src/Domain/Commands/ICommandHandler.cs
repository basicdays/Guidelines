namespace Guidelines.Core.Commands
{
	public interface ICommandHandler<in TCommand>
	{
		void Execute(TCommand commandMessage);
	}
}
