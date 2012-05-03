using System.Web.Mvc;
using Guidelines.WebUI.Controllers.ResultExtensions;
using Guidelines.WebUI.ActionResults;

namespace Guidelines.WebUI.Controllers.Expressions
{
	public static class MappedJsonQueryExpressions
	{
		public static QueryResult<TMessage, TResult> MappedJsonQuery<TMessage, TResult, TModel>(this Controller controller, TMessage message)
		{
			return new QueryResult<TMessage, TResult>(message,
			                                          (result, mapper) => controller.LargeJson(mapper.Map<TResult, TModel>(result)),
			                                          (input, mapper, error) => controller.Json(error)) { NoErrorState = true };
		}

		public static QueryResult<TMessage, TResult> MappedJsonQuery<TMessage, TResult, TModel>(this Controller controller, TModel message)
		{
			return new QueryResult<TMessage, TResult>(
				mapper => mapper.Map<TModel, TMessage>(message),
				(result, mapper) => controller.LargeJson(mapper.Map<TResult, TModel>(result)),
				(input, mapper, error) => controller.Json(error)) { NoErrorState = true };
		}
	}
}