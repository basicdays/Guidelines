using System;
using System.Web;
using System.Web.Mvc;
using Guidelines.Domain;
using Guidelines.Domain.Commands;
using Guidelines.WebUI.Errors;

namespace Guidelines.WebUI.ActionInvokers
{
    public class ResultInvoker<TActionMethodResult, TExecutedCommandResult> : IActionMethodResultInvoker<TActionMethodResult>
        where TActionMethodResult : IActionMethodResult
        where TExecutedCommandResult : CommandResult
    {
        private readonly IGenericMapper _mapper;
        private readonly IErrorAspect _errorAspect;
        private readonly IResultProcessor<TActionMethodResult, TExecutedCommandResult> _resultProcessor;

        public ResultInvoker(IResultProcessor<TActionMethodResult, TExecutedCommandResult> resultProcessor, IGenericMapper mapper, IErrorAspect errorAspect)
        {
            _resultProcessor = resultProcessor;
            _errorAspect = errorAspect;
            _mapper = mapper;
        }

        public ActionResult Invoke(TActionMethodResult actionMethodResult, ControllerContext context)
        {
            var modelState = context.Controller.ViewData.ModelState;
            Func<IGenericMapper, ErrorContext, ActionResult> actionResult = actionMethodResult.Failure;
            ErrorContext errorContext = null;

            if (modelState.IsValid)
            {
                TExecutedCommandResult executedCommandResult = _resultProcessor.ProcessCommand(actionMethodResult, _mapper);
                if (executedCommandResult.Successful)
                {
                    actionResult = _resultProcessor.HandleSuccess(executedCommandResult, actionMethodResult);
                }
                else
                {
                    errorContext = SetError(context, isAjaxRequest => 
                        actionMethodResult.NoErrorState || isAjaxRequest
                            ? _errorAspect.BuildErrorContext(context, executedCommandResult.Error)
                            : _errorAspect.SetErrorContext(context, executedCommandResult.Error));
                }
            }
            else
            {
                errorContext = SetError(context, isAjaxRequest =>
                        actionMethodResult.NoErrorState || isAjaxRequest
                            ? _errorAspect.BuildErrorContext(context, modelState)
                            : _errorAspect.SetErrorContext(context, modelState));
            }

            return actionResult(_mapper, errorContext);
        }

		private static ErrorContext SetError(ControllerContext context, Func<bool, ErrorContext> buildErrorContext)
		{
			bool isAjaxRequest = context.HttpContext.Request.IsAjaxRequest();

			InvalidateCache(context.HttpContext.Response.Cache);

			if (isAjaxRequest)
			{
				context.HttpContext.Response.StatusCode = 500;
				context.HttpContext.Response.ContentType = "application/json";
			}

			return buildErrorContext(isAjaxRequest);
		}

		private static void InvalidateCache(HttpCachePolicyBase cache)
		{
			cache.SetExpires(DateTime.UtcNow.AddDays(-1));
			cache.SetValidUntilExpires(false);
			cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
			cache.SetCacheability(HttpCacheability.NoCache);
			cache.SetNoStore();
		}
    }
}