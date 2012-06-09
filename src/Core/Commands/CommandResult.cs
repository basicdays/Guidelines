using System;

namespace Guidelines.Core.Commands
{
	public class CommandResult
	{
		private readonly Exception _errorMessage;

		public CommandResult(Exception errorMessage)
		{
			_errorMessage = errorMessage;
		}

		public bool Successful { get { return Error == null; } }

		public Exception Error
		{
			get { return _errorMessage; }
		}
	}
}
