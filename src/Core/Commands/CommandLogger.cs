namespace Guidelines.Core.Commands
{
    public class CommandLogger : ICommandPreprocessor
    {
		private readonly ILogger _logger;
        private readonly ITextSerializer _serializer;

		public CommandLogger(ILogger logger, ITextSerializer serializer)
        {
            _logger = logger;
            _serializer = serializer;
        }

        public void PreprocessCommand(object command)
        {
            _logger.Debug(_serializer.Serialize(command));
        }

        public bool CommandIsEligible(object command)
        {
            return command != null;
        }
    }
}
