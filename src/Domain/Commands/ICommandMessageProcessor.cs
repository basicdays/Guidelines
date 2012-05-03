namespace Guidelines.Domain.Commands
{
	public interface ICommandMessageProcessor
	{
		QueryResult<TResult> Execute<TCommand, TResult>(TCommand commandMessage, IQueryHandler<TCommand, TResult> handler);
		CommandResult Execute<TCommand>(TCommand commandMessage, ICommandHandler<TCommand> handler);
	}
}
