namespace Guidelines.Domain.Commands
{
	public interface IQueryHandler<in TCommand, out TResponse>
	{
		TResponse Execute(TCommand commandMessage);
	}
}
