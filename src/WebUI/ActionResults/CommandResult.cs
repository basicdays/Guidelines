using System;
using System.Web.Mvc;
using Guidelines.Core;
using Guidelines.WebUI.ActionInvokers;
using Guidelines.WebUI.Errors;

namespace Guidelines.WebUI.ActionResults
{
	public class CommandResult<TInput> : CommandResult
	{
		private Func<ActionResult> _success;
		private Func<TInput, IGenericMapper, ErrorContext, ActionResult> _failure;
		private TInput _message;

		public Func<IGenericMapper, TInput> MessageGetter { get; private set; }

		public override ActionResult Success()
		{
			return _success.Invoke();
		}

		public override ActionResult Failure(IGenericMapper mapper, ErrorContext errorContext)
		{
			return _failure.Invoke(_message, mapper, errorContext);
		}

		public CommandResult(Func<IGenericMapper, TInput> message, Func<ActionResult> success, Func<TInput, IGenericMapper, ErrorContext, ActionResult> failure)
		{
			MessageGetter = message;
			_success = success;
			_failure = failure;
		}

		public CommandResult(Func<IGenericMapper, TInput> message, Func<ActionResult> success, Func<TInput, IGenericMapper, ActionResult> failure)
		{
			MessageGetter = message;
			_success = success;
			_failure = (input, mapper, error) => failure(input, mapper);
		}

		public CommandResult(Func<IGenericMapper, TInput> message, Func<ActionResult> success, Func<TInput, ErrorContext, ActionResult> failure)
		{
			MessageGetter = message;
			_success = success;
			_failure = (input, mapper, error) => failure(input, error);
		}

		public CommandResult(Func<IGenericMapper, TInput> message, Func<ActionResult> success, Func<TInput, ActionResult> failure)
		{
			MessageGetter = message;
			_success = success;
			_failure = (input, mapper, error) => failure(input);
		}

		public CommandResult(TInput message, Func<ActionResult> success, Func<TInput, IGenericMapper, ErrorContext, ActionResult> failure)
		{
			MessageGetter = mapper => message;
			_success = success;
			_failure = failure;
		}

		public CommandResult(TInput message, Func<ActionResult> success, Func<TInput, IGenericMapper, ActionResult> failure)
		{
			MessageGetter = mapper => message;
			_success = success;
			_failure = (input, mapper, error) => failure(input, mapper);
		}

		public CommandResult(TInput message, Func<ActionResult> success, Func<TInput, ErrorContext, ActionResult> failure)
		{
			MessageGetter = mapper => message;
			_success = success;
			_failure = (input, mapper, error) => failure(input, error);
		}

		public CommandResult(TInput message, Func<ActionResult> success, Func<TInput, ActionResult> failure)
		{
			MessageGetter = mapper => message;
			_success = success;
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

	public abstract class CommandResult : ActionResult, IActionMethodResult
	{
		public abstract ActionResult Success();
		public abstract ActionResult Failure(IGenericMapper mapper, ErrorContext errorContext);
		public bool NoErrorState { get; set; }
	}
}
