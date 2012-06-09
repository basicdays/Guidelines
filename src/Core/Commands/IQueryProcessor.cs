namespace Guidelines.Core.Commands
{
	public interface IQueryProcessor<in TQuerryMessage, TResult>
	{
		QueryResult<TResult> Process(TQuerryMessage querryMessage);
	}
}
