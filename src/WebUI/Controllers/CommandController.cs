using System;
using System.Web.Mvc;
using Guidelines.Core;
using Guidelines.WebUI.ActionResults;
using Guidelines.WebUI.Errors;
using Guidelines.WebUI.Filters;

namespace Guidelines.WebUI.Controllers
{
	[AspectRegistrarFilter]
	public class CommandController : Controller
	{
		#region Query Shortcuts

		#region General Query Shortcuts

		public QueryResult<TMessage, TResult> Query<TMessage, TResult>(Func<IGenericMapper, TMessage> message, Func<TResult, IGenericMapper, ActionResult> success, Func<TMessage, IGenericMapper, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message, success, failure);
		}

		public QueryResult<TMessage, TResult> Query<TMessage, TResult>(Func<IGenericMapper, TMessage> message, Func<TResult, IGenericMapper, ActionResult> success, Func<TMessage, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message, success, failure);
		}

		public QueryResult<TMessage, TResult> Query<TMessage, TResult>(Func<IGenericMapper, TMessage> message, Func<TResult, ActionResult> success, Func<TMessage, IGenericMapper, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message, success, failure);
		}

		public QueryResult<TMessage, TResult> Query<TMessage, TResult>(Func<IGenericMapper, TMessage> message, Func<TResult, ActionResult> success, Func<TMessage, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message, success, failure);
		}

		public QueryResult<TMessage, TResult> Query<TMessage, TResult>(TMessage message, Func<TResult, IGenericMapper, ActionResult> success, Func<TMessage, IGenericMapper, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message, success, failure);
		}

		public QueryResult<TMessage, TResult> Query<TMessage, TResult>(TMessage message, Func<TResult, IGenericMapper, ActionResult> success, Func<TMessage, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message, success, failure);
		}

		public QueryResult<TMessage, TResult> Query<TMessage, TResult>(TMessage message, Func<TResult, ActionResult> success, Func<TMessage, IGenericMapper, ErrorContext, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message, success, failure);
		}

		public QueryResult<TMessage, TResult> Query<TMessage, TResult>(TMessage message, Func<TResult, ActionResult> success, Func<TMessage, IGenericMapper, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message, success, failure);
		}

		public QueryResult<TMessage, TResult> Query<TMessage, TResult>(TMessage message, Func<TResult, ActionResult> success, Func<TMessage, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message, success, failure);
		}

		public QueryResult<TMessage, TMessage> Query<TMessage>(TMessage message, Func<TMessage, ActionResult> result)
		{
			return new QueryResult<TMessage, TMessage>(message, result, result);
		}

		#endregion

		#region Mapped Query Shortcuts

		public QueryResult<TMessage, TResult> MappedQuery<TMessage, TResult, TModel>(TMessage message, Func<TModel, ActionResult> success, Func<TMessage, ErrorContext, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message,
				(result, mapper) => success(mapper.Map<TResult, TModel>(result)),
				failure);
		}

		public QueryResult<TMessage, TResult> MappedQuery<TMessage, TResult, TModel>(TMessage message, Func<TModel, ActionResult> success)
		{
			return new QueryResult<TMessage, TResult>(message,
				(result, mapper) => success(mapper.Map<TResult, TModel>(result)),
				(model, mapper, error) => View("Error", error));
		}

		public QueryResult<TMessage, TResult> MappedQuery<TMessage, TResult, TModel>(TMessage message, Func<TModel, ActionResult> success, Func<TMessage, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(message,
				(result, mapper) => success(mapper.Map<TResult, TModel>(result)),
				failure);
		}

		public QueryResult<TMessage, TResult> MappedQuery<TMessage, TResult, TModel>(TModel message, Func<TResult, ActionResult> success, Func<TModel, ActionResult> failure)
		{
			return new QueryResult<TMessage, TResult>(
				mapper => mapper.Map<TModel, TMessage>(message),
				(result, mapper) => success(result),
				f => failure(message));
		}

		#endregion

		#region Json Query Shortcuts

		public QueryResult<TMessage, TResult> JsonQuery<TMessage, TResult>(TMessage message, Func<TResult, ActionResult> success)
		{
			return new QueryResult<TMessage, TResult>(message, success,
				(input, error) => Json(error)) { NoErrorState = true };
		}

		public QueryResult<TMessage, TResult> MappedJsonQuery<TMessage, TResult, TModel>(TMessage message)
		{
			return new QueryResult<TMessage, TResult>(message,
				(result, mapper) => DynamicView(mapper.Map<TResult, TModel>(result)),
				(input, mapper, error) => Json(error)) { NoErrorState = true };
		}

		public QueryResult<TMessage, TResult> MappedJsonQuery<TMessage, TResult, TModel>(TModel message)
		{
			return new QueryResult<TMessage, TResult>(
				mapper => mapper.Map<TModel, TMessage>(message),
				(result, mapper) => DynamicView(mapper.Map<TResult, TModel>(result)),
				(input, mapper, error) => Json(error)) { NoErrorState = true };
		}

		#endregion

		#endregion

		#region Command Shortcuts

		public CommandResult<TInput> Command<TInput>(TInput message, Func<ActionResult> success, Func<TInput, ActionResult> failure)
		{
			return new CommandResult<TInput>(message, success, failure);
		}

		public CommandResult<TInput> Command<TInput>(TInput message, Func<ActionResult> success, Func<TInput, ErrorContext, ActionResult> failure)
		{
			return new CommandResult<TInput>(message, success, failure);
		}

		public CommandResult<TInput> AjaxCommand<TInput>(TInput message)
		{
			return new CommandResult<TInput>(message,
				() => Json("Success"),
				(input, mapper, error) => Json(error)) { NoErrorState = true };
		}

		public CommandResult<TInput> Command<TInput>(TInput message, Func<ActionResult> success)
		{
			return new CommandResult<TInput>(message, success, fail => success());
		}

		#endregion

		#region Command and Query Builders

		protected CommandBuilder Command()
		{
			return new CommandBuilder();
		}

		protected CommandBuilder<TInput> Command<TInput>()
		{
			return new CommandBuilder<TInput>();
		}

		protected QueryBuilder Query()
		{
			return new QueryBuilder();
		}

		protected QueryBuilder<TInput> Query<TInput>()
		{
			return new QueryBuilder<TInput>();
		}

		protected QueryBuilder<TInput, TResult> Query<TInput, TResult>()
		{
			return new QueryBuilder<TInput, TResult>();
		}

		#endregion

		protected ActionResult ErrorView(ErrorContext error)
		{
			return View("Error", error);
		}

		protected ActionResult JsonError(ErrorContext error)
		{
			return Content(error.ErrorMessage);
		}

		protected DynamicView DynamicView(object model)
		{
			return new DynamicView(model);
		}

		protected DownloadResult Download(string filePath, string fileName)
		{
			return new DownloadResult(filePath, fileName);
		}
	}
}
