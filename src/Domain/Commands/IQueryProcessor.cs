namespace Guidelines.Domain.Commands
{
	public interface IQueryProcessor<in TQuerryMessage, TResult>
	{
		QueryResult<TResult> Process(TQuerryMessage querryMessage);
	}
}
