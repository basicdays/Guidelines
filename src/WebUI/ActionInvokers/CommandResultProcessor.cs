using System;
using System.Web.Mvc;
using Guidelines.Core;
using Guidelines.Core.Commands;
using Guidelines.WebUI.ActionResults;
using Guidelines.WebUI.Errors;
using CommandResult = Guidelines.Core.Commands.CommandResult;

namespace Guidelines.WebUI.ActionInvokers
{
    public class CommandResultProcessor<TInput> : IResultProcessor<CommandResult<TInput>, CommandResult>
    {
        private readonly ICommandProcessor<TInput> _commandProcessor;

        public CommandResultProcessor(ICommandProcessor<TInput> commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }

        public CommandResult ProcessCommand(CommandResult<TInput> actionMethodResult, IGenericMapper mapper)
        {
            return _commandProcessor.Process(actionMethodResult.GetMessage(mapper));
        }

        public Func<IGenericMapper, ErrorContext, ActionResult> HandleSuccess(CommandResult executedCommandResult, CommandResult<TInput> actionMethodResult)
        {
            return (mapper, error) => actionMethodResult.Success();
        }
    }
}