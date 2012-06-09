using System;
using System.Web.Mvc;
using Guidelines.Core;
using Guidelines.WebUI.ActionInvokers;
using Guidelines.WebUI.Errors;

namespace Guidelines.WebUI.ActionResults
{
	public class QueryResult<TInput, TResult> : QueryResult
	{
		private readonly Func<TResult, IGenericMapper, ActionResult> _success;
		private readonly Func<TInput, IGenericMapper, ErrorContext, ActionResult> _failure;
		private TInput _message;

		public Func<IGenericMapper, TInput> MessageGetter { get; private set; }

		public TResult Result { get; set; }

		public override ActionResult Success(IGenericMapper mapper)
		{
			return _success(Result, mapper);
		}

		public override ActionResult Failure(IGenericMapper mapper, ErrorContext errorContext)
		{
			return _failure(_message, mapper, errorContext);
		}

		public QueryResult(Func<IGenericMapper, TInput> message, Func<TResult, IGenericMapper, ActionResult> success, Func<TInput, IGenericMapper, ErrorContext, ActionResult> failure)
		{
			MessageGetter = message;
			_success = success;
			_failure = failure;
		}

		public QueryResult(Func<IGenericMapper, TInput> message, Func<TResult, IGenericMapper, ActionResult> success, Func<TInput, ErrorContext, ActionResult> failure)
		{
			MessageGetter = message;
			_success = success;
			_failure = (input, mapper, error) => failure(input, error);
		}

		public QueryResult(Func<IGenericMapper, TInput> message, Func<TResult, IGenericMapper, ActionResult> success, Func<TInput, IGenericMapper, ActionResult> failure)
		{
			MessageGetter = message;
			_success = success;
			_failure = (input, mapper, error) => failure(input, mapper);
		}

		public QueryResult(Func<IGenericMapper, TInput> message, Func<TResult, IGenericMapper, ActionResult> success, Func<TInput, ActionResult> failure)
		{
			MessageGetter = message;
			_success = success;
			_failure = (input, mapper, error) => failure(input);
		}

		public QueryResult(Func<IGenericMapper, TInput> message, Func<TResult, ActionResult> success, Func<TInput, IGenericMapper, ErrorContext, ActionResult> failure)
		{
			MessageGetter = message;
			_success = (result, mapper) => success(result);
			_failure = failure;
		}

		public QueryResult(Func<IGenericMapper, TInput> message, Func<TResult, ActionResult> success, Func<TInput, ErrorContext, ActionResult> failure)
		{
			MessageGetter = message;
			_success = (result, mapper) => success(result);
			_failure = (input, mapper, error) => failure(input, error);
		}

		public QueryResult(Func<IGenericMapper, TInput> message, Func<TResult, ActionResult> success, Func<TInput, IGenericMapper, ActionResult> failure)
		{
			MessageGetter = message;
			_success = (result, mapper) => success(result);
			_failure = (input, mapper, error) => failure(input, mapper);
		}

		public QueryResult(Func<IGenericMapper, TInput> message, Func<TResult, ActionResult> success, Func<TInput, ActionResult> failure)
		{
			MessageGetter = message;
			_success = (result, mapper) => success(result);
			_failure = (input, mapper, error) => failure(input);
		}

		public QueryResult(TInput message, Func<TResult, IGenericMapper, ActionResult> success, Func<TInput, IGenericMapper, ErrorContext, ActionResult> failure)
		{
			MessageGetter = mapper => message;
			_success = success;
			_failure = failure;
		}

		public QueryResult(TInput message, Func<TResult, IGenericMapper, ActionResult> success, Func<TInput, ErrorContext, ActionResult> failure)
		{
			MessageGetter = mapper => message;
			_success = success;
			_failure = (input, mapper, error) => failure(input, error);
		}

		public QueryResult(TInput message, Func<TResult, IGenericMapper, ActionResult> success, Func<TInput, IGenericMapper, ActionResult> failure)
		{
			MessageGetter = mapper => message;
			_success = success;
			_failure = (input, mapper, error) => failure(input, mapper);
		}

		public QueryResult(TInput message, Func<TResult, IGenericMapper, ActionResult> success, Func<TInput, ActionResult> failure)
		{
			MessageGetter = mapper => message;
			_success = success;
			_failure = (input, mapper, error) => failure(input);
		}

		public QueryResult(TInput message, Func<TResult, ActionResult> success, Func<TInput, IGenericMapper, ErrorContext, ActionResult> failure)
		{
			MessageGetter = mapper => message;
			_success = (result, mapper) => success(result);
			_failure = failure;
		}

		//public QueryResult(TInput message, Func<TResult, ActionResult> success, Func<TInput, ErrorContext, ActionResult> failure)
		//{
		//    MessageGetter = mapper => message;
		//    _success = (result, mapper) => success(result);
		//    _failure = (input, mapper, error) => failure(input, error);
		//}

		public QueryResult(TInput message, Func<TResult, ActionResult> success, Func<TInput, IGenericMapper, ActionResult> failure)
		{
			MessageGetter = mapper => message;
			_success = (result, mapper) => success(result);
			_failure = (input, mapper, error) => failure(input, mapper);
		}

		public QueryResult(TInput message, Func<TResult, ActionResult> success, Func<TInput, ActionResult> failure)
		{
			MessageGetter = mapper => message;
			_success = (result, mapper) => success(result);
			_failure = (input, mapper, error) => failure(input);
		}

		public TInput GetMessage(IGenericMapper mapper)
		{
			_message = MessageGetter(mapper);
			return _message;
		}

		public override void ExecuteResult(ControllerContext context)
		{

		}
	}

	public abstract class QueryResult : ActionResult, IActionMethodResult
	{
		public abstract ActionResult Success(IGenericMapper mapper);
		public abstract ActionResult Failure(IGenericMapper mapper, ErrorContext errorContext);
		public bool NoErrorState { get; set; }
	}
}
