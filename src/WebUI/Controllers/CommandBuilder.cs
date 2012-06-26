using System;
using System.Web.Mvc;
using Guidelines.Core;
using Guidelines.WebUI.ActionResults;
using Guidelines.WebUI.Errors;

namespace Guidelines.WebUI.Controllers
{
	public class CommandBuilder
	{
		public CommandBuilder<TInputModel> Handle<TInputModel>()
		{
			return new CommandBuilder<TInputModel>();
		}
	}

	public class CommandBuilder<TInputModel>
	{
		public CommandBuilderStageTwo<TInputModel> OnSuccesExecuteResult(Func<ActionResult> onSuccess)
		{
			return new CommandBuilderStageTwo<TInputModel>(onSuccess);
		}

		public CommandBuilderStageTwo<TInputModel> OnSuccesExecuteResult(ActionResult actionResult)
		{
			return new CommandBuilderStageTwo<TInputModel>(() => actionResult);
		}
	}

	public class CommandBuilderStageTwo<TInputModel>
	{
		private readonly Func<ActionResult> _success;

		public CommandBuilderStageTwo(Func<ActionResult> success)
		{
			_success = success;
		}

		public CommandBuilderStageThree<TInputModel> UseSuccessPathOnFailure()
		{
			return new CommandBuilderStageThree<TInputModel>(_success,
				(input, mapper, error) => _success());
		}

		public CommandBuilderStageThree<TInputModel> OnFailureExecuteResult<TMapFrom>(Func<TMapFrom, ActionResult> onFailure)
		{
			return new CommandBuilderStageThree<TInputModel>(_success,
				(input, mapper, error) => onFailure(mapper.Map<TInputModel, TMapFrom>(input)));
		}

		public CommandBuilderStageThree<TInputModel> OnFailureExecuteResult(Func<TInputModel, ActionResult> onFailure)
		{
			return new CommandBuilderStageThree<TInputModel>(_success,
				(input, mapper, error) => onFailure(input));
		}

		public CommandBuilderStageThree<TInputModel> OnFailureHandleError(Func<ErrorContext, ActionResult> onFailure)
		{
			return new CommandBuilderStageThree<TInputModel>(_success,
				(input, mapper, error) => onFailure(error));
		}

		public CommandBuilderStageThree<TInputModel> OnFailureExecuteResult(Func<ActionResult> onFailure)
		{
			return new CommandBuilderStageThree<TInputModel>(_success,
				(input, mapper, error) => onFailure());
		}

		public CommandBuilderStageThree<TInputModel> OnFailureExecuteResult(ActionResult actionResult)
		{
			return new CommandBuilderStageThree<TInputModel>(_success,
				(input, mapper, error) => actionResult);
		}
	}

	public class CommandBuilderStageThree<TInputModel>
	{
		private Func<IGenericMapper, TInputModel> _message;
		private readonly Func<ActionResult> _success;
		private readonly Func<TInputModel, IGenericMapper, ErrorContext, ActionResult> _failure;

		public CommandBuilderStageThree(Func<ActionResult> success, Func<TInputModel, IGenericMapper, ErrorContext, ActionResult> failure)
		{
			_success = success;
			_failure = failure;
		}

		private CommandBuilderStageThree<TInputModel> WithMessage(TInputModel command)
		{
			_message = mapper => command;
			return this;
		}

		private CommandBuilderStageThree<TInputModel> WithMessage<TMapFrom>(TMapFrom message)
		{
			_message = mapper => mapper.Map<TMapFrom, TInputModel>(message);
			return this;
		}

		#region Command Execution

		private CommandResult<TInputModel> Run()
		{
			return new CommandResult<TInputModel>(_message, _success, _failure);
		}

		public CommandResult<TInputModel> ExecuteWith(TInputModel command)
		{
			return WithMessage(command).Run();
		}

		public CommandResult<TInputModel> ExecuteWith<TMapFrom>(TMapFrom message)
		{
			return WithMessage(message).Run();
		}

		#endregion
	}
}