using System;

namespace Guidelines.Domain.Commands
{
	public class QueryResult<TResult> : CommandResult
	{
		public QueryResult(Exception errorMessage, TResult result)
			: base(errorMessage)
		{
			Result = result;
		}

		public TResult Result { get; private set; }
	}
}
