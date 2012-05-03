namespace Guidelines.Domain.Commands
{
	public class CommandProcessor<TCommandMessage> : ICommandProcessor<TCommandMessage>
	{
		private readonly ICommandMessageProcessor _commandProcessor;
		private readonly ICommandHandler<TCommandMessage> _commandHandler;

		public CommandProcessor(ICommandMessageProcessor commandProcessor, ICommandHandler<TCommandMessage> commandHandler)
		{
			_commandProcessor = commandProcessor;
			_commandHandler = commandHandler;
		}

		public CommandResult Process(TCommandMessage commandMessage)
		{
			return _commandProcessor.Execute(commandMessage, _commandHandler);
		}
	}
}
