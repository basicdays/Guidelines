using System;
using System.Web.Mvc;
using Guidelines.Core;
using Guidelines.WebUI.ActionResults;
using Guidelines.WebUI.Errors;

namespace Guidelines.WebUI.Controllers
{
	public class QueryBuilder
	{
		public QueryBuilder<TInputModel> Handle<TInputModel>()
		{
			return new QueryBuilder<TInputModel>();
		}
	}

	public class QueryBuilder<TInput>
	{
		public QueryBuilder<TInput, TResult> WithResult<TResult>()
		{
			return new QueryBuilder<TInput, TResult>();
		}
	}

	public class QueryBuilder<TInputModel, TResult>
	{
		public QueryBuilderStageTwo<TInputModel, TResult> OnSuccesExecuteResult(Func<TResult, IGenericMapper, ActionResult> onSuccess)
		{
			return new QueryBuilderStageTwo<TInputModel, TResult>(onSuccess);
		}

		public QueryBuilderStageTwo<TInputModel, TResult> OnSuccesExecuteResult(Func<TResult, ActionResult> onSuccess)
		{
			return new QueryBuilderStageTwo<TInputModel, TResult>((result, mapper) => onSuccess(result));
		}

		public QueryBuilderStageTwo<TInputModel, TResult> OnSuccesExecuteResult<TMappedResult>(Func<TMappedResult, ActionResult> onSuccess)
		{
			return new QueryBuilderStageTwo<TInputModel, TResult>((result, mapper) => onSuccess(mapper.Map<TResult, TMappedResult>(result)));
		}

		public QueryBuilderStageTwo<TInputModel, TResult> OnSuccesExecuteResult(Func<ActionResult> onSuccess)
		{
			return new QueryBuilderStageTwo<TInputModel, TResult>((result, mapper) => onSuccess());
		}

		public QueryBuilderStageTwo<TInputModel, TResult> OnSuccesExecuteResult(ActionResult actionResult)
		{
			return new QueryBuilderStageTwo<TInputModel, TResult>((result, mapper) => actionResult);
		}
	}

	public class QueryBuilderStageTwo<TInputModel, TResult>
	{
		private readonly Func<TResult, IGenericMapper, ActionResult> _success;

		public QueryBuilderStageTwo(Func<TResult, IGenericMapper, ActionResult> success)
		{
			_success = success;
		}

		public QueryBuilderStageThree<TInputModel, TResult> OnFailureExecuteResult<TMapFrom>(Func<TMapFrom, ActionResult> onFailure)
		{
			return new QueryBuilderStageThree<TInputModel, TResult>(_success,
				(input, mapper, error) => onFailure(mapper.Map<TInputModel, TMapFrom>(input)));
		}

		public QueryBuilderStageThree<TInputModel, TResult> OnFailureExecuteResult(Func<TInputModel, ActionResult> onFailure)
		{
			return new QueryBuilderStageThree<TInputModel, TResult>(_success,
				(input, mapper, error) => onFailure(input));
		}

		public QueryBuilderStageThree<TInputModel, TResult> OnFailureHandleError(Func<ErrorContext, ActionResult> onFailure)
		{
			return new QueryBuilderStageThree<TInputModel, TResult>(_success,
				(input, mapper, error) => onFailure(error));
		}

		public QueryBuilderStageThree<TInputModel, TResult> OnFailureExecuteResult(Func<ActionResult> onFailure)
		{
			return new QueryBuilderStageThree<TInputModel, TResult>(_success,
				(input, mapper, error) => onFailure());
		}

		public QueryBuilderStageThree<TInputModel, TResult> OnFailureExecuteResult(ActionResult actionResult)
		{
			return new QueryBuilderStageThree<TInputModel, TResult>(_success,
				(input, mapper, error) => actionResult);
		}
	}

	public class QueryBuilderStageThree<TInputModel, TResult>
	{
		private Func<IGenericMapper, TInputModel> _message;

		private readonly Func<TResult, IGenericMapper, ActionResult> _success;
		private readonly Func<TInputModel, IGenericMapper, ErrorContext, ActionResult> _failure;

		public QueryBuilderStageThree(Func<TResult, IGenericMapper, ActionResult> success, Func<TInputModel, IGenericMapper, ErrorContext, ActionResult> failure)
		{
			_success = success;
			_failure = failure;
		}

		private QueryBuilderStageThree<TInputModel, TResult> WithMessage(TInputModel command)
		{
			_message = mapper => command;
			return this;
		}

		private QueryBuilderStageThree<TInputModel, TResult> WithMessage<TMapFrom>(TMapFrom message)
		{
			_message = mapper => mapper.Map<TMapFrom, TInputModel>(message);
			return this;
		}

		#region Query Execution

		private QueryResult<TInputModel, TResult> Run()
		{
			return new QueryResult<TInputModel, TResult>(_message, _success, _failure);
		}

		public QueryResult<TInputModel, TResult> ExecuteWith(TInputModel command)
		{
			return WithMessage(command).Run();
		}

		public QueryResult<TInputModel, TResult> ExecuteWith<TMapFrom>(TMapFrom message)
		{
			return WithMessage(message).Run();
		}

		#endregion
	}
}