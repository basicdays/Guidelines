using System;
using System.Web.Mvc;
using Guidelines.Core;
using Guidelines.WebUI.ActionResults;
using Guidelines.WebUI.Errors;

namespace Guidelines.WebUI.Controllers.Expressions
{
	public static class QueryExpressions
	{
		public static QueryResult<TMessage, TResult> Query<TMessage, TResult>(this Controller controller, Func<IGenericMapper, TMessage> message, Func<TResult, IGenericMapper, ActionResult> success, Func<TMessage, IGenericMapper, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message, success, failure);
		}

		public static QueryResult<TMessage, TResult> Query<TMessage, TResult>(this Controller controller, Func<IGenericMapper, TMessage> message, Func<TResult, IGenericMapper, ActionResult> success, Func<TMessage, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message, success, failure);
		}

		public static QueryResult<TMessage, TResult> Query<TMessage, TResult>(this Controller controller, Func<IGenericMapper, TMessage> message, Func<TResult, ActionResult> success, Func<TMessage, IGenericMapper, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message, success, failure);
		}

		public static QueryResult<TMessage, TResult> Query<TMessage, TResult>(this Controller controller, Func<IGenericMapper, TMessage> message, Func<TResult, ActionResult> success, Func<TMessage, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message, success, failure);
		}

		public static QueryResult<TMessage, TResult> Query<TMessage, TResult>(this Controller controller, TMessage message, Func<TResult, IGenericMapper, ActionResult> success, Func<TMessage, IGenericMapper, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message, success, failure);
		}

		public static QueryResult<TMessage, TResult> Query<TMessage, TResult>(this Controller controller, TMessage message, Func<TResult, IGenericMapper, ActionResult> success, Func<TMessage, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message, success, failure);
		}

		public static QueryResult<TMessage, TResult> Query<TMessage, TResult>(this Controller controller, TMessage message, Func<TResult, ActionResult> success, Func<TMessage, IGenericMapper, ErrorContext, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message, success, failure);
		}

		public static QueryResult<TMessage, TResult> Query<TMessage, TResult>(this Controller controller, TMessage message, Func<TResult, ActionResult> success, Func<TMessage, IGenericMapper, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message, success, failure);
		}

		public static QueryResult<TMessage, TResult> Query<TMessage, TResult>(this Controller controller, TMessage message, Func<TResult, ActionResult> success, Func<TMessage, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message, success, failure);
		}

		public static QueryResult<TMessage, TMessage> Query<TMessage>(this Controller controller, TMessage message, Func<TMessage, ActionResult> result)
		{
			return new QueryResult<TMessage, TMessage>(message, result, result);
		}
	}
}