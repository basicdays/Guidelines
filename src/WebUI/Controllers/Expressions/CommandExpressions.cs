using System;
using System.Web.Mvc;
using Guidelines.WebUI.Controllers.ResultExtensions;
using Guidelines.WebUI.ActionResults;
using Guidelines.WebUI.Errors;

namespace Guidelines.WebUI.Controllers.Expressions
{
	public static class CommandExpressions
	{
		public static CommandResult<TInput> Command<TInput>(this Controller controller, TInput message, Func<ActionResult> success, Func<TInput, ActionResult> failure)
		{
			return new CommandResult<TInput>(message, success, failure);
		}

		public static CommandResult<TInput> Command<TInput>(this Controller controller, TInput message, Func<ActionResult> success, Func<TInput, ErrorContext, ActionResult> failure)
		{
			return new CommandResult<TInput>(message, success, failure);
		}

		public static CommandResult<TInput> AjaxCommand<TInput>(this Controller controller, TInput message)
		{
			return new CommandResult<TInput>(message,
			                                 () => controller.Json("Success"),
			                                 (input, mapper, error) => controller.Json(error)) { NoErrorState = true };
		}

		public static CommandResult<TInput> Command<TInput>(this Controller controller, TInput message, Func<ActionResult> success)
		{
			return new CommandResult<TInput>(message, success, fail => success());
		}
	}
}