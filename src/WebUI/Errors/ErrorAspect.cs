using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Guidelines.WebUI.Properties;
using Guidelines.Domain.Validation;
using Guidelines.WebUI.Session;

namespace Guidelines.WebUI.Errors
{
    public class ErrorAspect : IErrorAspect, IContextAction
    {
        public const string ErrorContextKey = "__errorContext";
		public const string GeneralErrorKey = "__generalError";
        public const string GeneralWarningKey = "__generalWarning";
        public const string GeneralMessageKey = "__generalMessage";

        public ErrorContext SetErrorContext(ControllerContext context, Exception ex)
        {
            ErrorContext errorContext = BuildErrorContext(context, ex);

            context.Controller.TempData[ErrorContextKey] = errorContext;

            return errorContext;
        }

        public ErrorContext SetErrorContext(ControllerContext context, ModelStateDictionary modelState)
        {
            var errorContext = BuildErrorContext(context, modelState);

            context.Controller.TempData[ErrorContextKey] = errorContext;

            return errorContext;
        }

        public ErrorContext BuildErrorContext(ControllerContext context, Exception ex)
        {
            var validationEx = ex as ValidationEngineException;
            var additionalMessages = new List<string>();

            if (validationEx != null)
            {
                foreach (var error in validationEx.Errors)
                {
                    if (error.Name != null)
                    {
                        context.Controller.ViewData.ModelState.AddModelError(error.Name, error.Message);
                    }

                    additionalMessages.Add(error.Message);
                }
            }

            return CreateContext(context, ex, additionalMessages, context.Controller.ViewData.ModelState);
        }

        public ErrorContext BuildErrorContext(ControllerContext context, ModelStateDictionary modelState)
        {
            var errors = modelState.Values
                .SelectMany(state => state.Errors)
				.Select(error => string.IsNullOrEmpty(error.ErrorMessage) ? Resources.Error_InputNotInTheCorrectFormat : error.ErrorMessage);

			ErrorContext errorContext = CreateContext(context, new Exception(Resources.Error_ValidationError), errors, modelState);

            return errorContext;
        }

        private static ErrorContext CreateContext(ControllerContext context, Exception ex, IEnumerable<string> additionalMessages, ModelStateDictionary modelState)
        {
            var actionName = context.RouteData.Values["controller"].ToString();
            var controllerName = context.RouteData.Values["action"].ToString();

            var errorContext = new ErrorContext(ex, controllerName, actionName, additionalMessages, modelState);

            return errorContext;
        }

        public void TakePreRenderAction(ActionExecutedContext context)
        {
            var errorContext = (ErrorContext)context.Controller.TempData[ErrorContextKey];
            if (errorContext != null && context.Result is ViewResult)
            {
                errorContext.AddModelstateErrors(context.Controller.ViewData.ModelState);
            }
        }
    }
}