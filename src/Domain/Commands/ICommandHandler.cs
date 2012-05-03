namespace Guidelines.Domain.Commands
{
	public interface ICommandHandler<in TCommand>
	{
		void Execute(TCommand commandMessage);
	}
}
