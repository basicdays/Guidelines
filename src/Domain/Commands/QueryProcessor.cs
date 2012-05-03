namespace Guidelines.Domain.Commands
{
	public class QueryProcessor<TQuerryMessage, TResult> : IQueryProcessor<TQuerryMessage, TResult>
	{
		private readonly ICommandMessageProcessor _commandProcessor;
		private readonly IQueryHandler<TQuerryMessage, TResult> _queryHandler;

		public QueryProcessor(ICommandMessageProcessor commandProcessor, IQueryHandler<TQuerryMessage, TResult> queryHandler)
		{
			_commandProcessor = commandProcessor;
			_queryHandler = queryHandler;
		}

		public QueryResult<TResult> Process(TQuerryMessage querryMessage)
		{
			return _commandProcessor.Execute(querryMessage, _queryHandler);
		}
	}
}
