using System;
using System.Web.Mvc;
using Guidelines.Domain;
using Guidelines.Domain.Commands;
using Guidelines.WebUI.ActionResults;
using Guidelines.WebUI.Errors;

namespace Guidelines.WebUI.ActionInvokers
{
    public class QueryResultProcessor<TInput, TResult> : IResultProcessor<QueryResult<TInput, TResult>, QueryResult<TResult>>
    {
        private readonly IQueryProcessor<TInput, TResult> _commandProcessor;

        public QueryResultProcessor(IQueryProcessor<TInput, TResult> commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }

        public QueryResult<TResult> ProcessCommand(QueryResult<TInput, TResult> actionMethodResult, IGenericMapper mapper)
        {
            return _commandProcessor.Process(actionMethodResult.GetMessage(mapper));
        }

        public Func<IGenericMapper, ErrorContext, ActionResult> HandleSuccess(QueryResult<TResult> executedCommandResult, QueryResult<TInput, TResult> actionMethodResult)
        {
            actionMethodResult.Result = executedCommandResult.Result;
            return (mapper, error) => actionMethodResult.Success(mapper);
        }
    }
}