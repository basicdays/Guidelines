using System;
using System.Web.Mvc;
using Guidelines.Domain;
using Guidelines.Domain.Commands;
using Guidelines.WebUI.ActionResults;
using Guidelines.WebUI.Errors;
using CommandResult = Guidelines.Domain.Commands.CommandResult;

namespace Guidelines.WebUI.ActionInvokers
{
    public class CommandResultProcessor<TInput> : IResultProcessor<CommandResult<TInput>, Domain.Commands.CommandResult>
    {
        private readonly ICommandProcessor<TInput> _commandProcessor;

        public CommandResultProcessor(ICommandProcessor<TInput> commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }

        public Domain.Commands.CommandResult ProcessCommand(CommandResult<TInput> actionMethodResult, IGenericMapper mapper)
        {
            return _commandProcessor.Process(actionMethodResult.GetMessage(mapper));
        }

        public Func<IGenericMapper, ErrorContext, ActionResult> HandleSuccess(Domain.Commands.CommandResult executedCommandResult, CommandResult<TInput> actionMethodResult)
        {
            return (mapper, error) => actionMethodResult.Success();
        }
    }
}