using System;
using System.Web.Mvc;
using Guidelines.WebUI.Controllers.ResultExtensions;
using Guidelines.WebUI.ActionResults;
using Guidelines.WebUI.Errors;

namespace Guidelines.WebUI.Controllers.Expressions
{
	public static class MappedQueryExpressions
	{
		public static QueryResult<TMessage, TResult> MappedQuery<TMessage, TResult, TModel>(this Controller controller, TMessage message, Func<TModel, ActionResult> success, Func<TMessage, ErrorContext, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message,
			                                          (result, mapper) => success(mapper.Map<TResult, TModel>(result)),
			                                          failure);
		}

		public static QueryResult<TMessage, TResult> MappedQuery<TMessage, TResult, TModel>(this Controller controller, TMessage message, Func<TModel, ActionResult> success)
		{
			return new QueryResult<TMessage, TResult>(message,
			                                          (result, mapper) => success(mapper.Map<TResult, TModel>(result)),
			                                          (model, mapper, error) => controller.View("Error", error));
		}

		public static QueryResult<TMessage, TResult> MappedQuery<TMessage, TResult, TModel>(this Controller controller, TMessage message, Func<TModel, ActionResult> success, Func<TMessage, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message,
			                                          (result, mapper) => success(mapper.Map<TResult, TModel>(result)),
			                                          failure);
		}

		public static QueryResult<TMessage, TResult> MappedQuery<TMessage, TResult, TModel>(this Controller controller, TModel message, Func<TResult, ActionResult> success, Func<TModel, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(
				mapper => mapper.Map<TModel, TMessage>(message),
				(result, mapper) => success(result),
				f => failure(message));
		}
	}
}