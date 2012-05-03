using System;
using System.Web.Mvc;
using Guidelines.WebUI.Controllers.ResultExtensions;
using Guidelines.WebUI.ActionResults;

namespace Guidelines.WebUI.Controllers.Expressions
{
	public static class JsonQueryExpressions
	{
		public static QueryResult<TMessage, TResult> JsonQuery<TMessage, TResult>(this Controller controller, TMessage message, Func<TResult, ActionResult> success)
		{
			return new QueryResult<TMessage, TResult>(message, success,
			                                          (input, error) => controller.Json(error)) { NoErrorState = true };
		}
	}
}