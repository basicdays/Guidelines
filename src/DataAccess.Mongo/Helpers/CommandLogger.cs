using Guidelines.Core;
using Guidelines.Core.Commands;
using MongoDB.Bson;

namespace Guidelines.DataAccess.Mongo.Helpers
{
	public class CommandLogger : ICommandPreprocessor
	{
		private readonly ILogger _logger;

		public CommandLogger(ILogger logger)
		{
			_logger = logger;
		}

		public void PreprocessCommand(object command)
		{
			_logger.Debug(command.ToJson());
		}

		public bool CommandIsEligible(object command)
		{
			return command != null;
		}
	}
}
