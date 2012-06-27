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
        public class MappedQueryBuilder<TMapTo>
        {
            public QueryBuilderStageTwo<TInputModel, TResult> OnSuccessBuildResultWith(Func<TMapTo, ActionResult> onSuccess)
            {
                return new QueryBuilderStageTwo<TInputModel, TResult>((result, mapper) => onSuccess(mapper.Map<TResult, TMapTo>(result)));
            }
        }

        public MappedQueryBuilder<TMappedResult> MapResultTo<TMappedResult>()
        {
            return new MappedQueryBuilder<TMappedResult>();
        }

        public QueryBuilderStageTwo<TInputModel, TResult> OnSuccessBuildResultWith(Func<TResult, IGenericMapper, ActionResult> onSuccess)
        {
            return new QueryBuilderStageTwo<TInputModel, TResult>(onSuccess);
        }

        public QueryBuilderStageTwo<TInputModel, TResult> OnSuccessBuildResultWith(Func<TResult, ActionResult> onSuccess)
        {
            return new QueryBuilderStageTwo<TInputModel, TResult>((result, mapper) => onSuccess(result));
        }

        public QueryBuilderStageTwo<TInputModel, TResult> OnSuccessBuildMappedResultWith<TMappedResult>(Func<TMappedResult, ActionResult> onSuccess)
        {
            return new QueryBuilderStageTwo<TInputModel, TResult>((result, mapper) => onSuccess(mapper.Map<TResult, TMappedResult>(result)));
        }

        public QueryBuilderStageTwo<TInputModel, TResult> OnSuccessUseResultFrom(Func<ActionResult> onSuccess)
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
        public class MappedQueryBuilderStageTwo<TMapTo>
        {
            private readonly Func<TResult, IGenericMapper, ActionResult> _mappedSuccess;

            public MappedQueryBuilderStageTwo(Func<TResult, IGenericMapper, ActionResult> success)
            {
                _mappedSuccess = success;
            }

            public QueryBuilderStageThree<TInputModel, TResult> OnFailureBuildResultWith(Func<TMapTo, ActionResult> onFailure)
            {
                return new QueryBuilderStageThree<TInputModel, TResult>(_mappedSuccess,
                    (input, mapper, error) => onFailure(mapper.Map<TInputModel, TMapTo>(input)));
            }
        }

        private readonly Func<TResult, IGenericMapper, ActionResult> _success;

        public QueryBuilderStageTwo(Func<TResult, IGenericMapper, ActionResult> success)
        {
            _success = success;
        }

        public MappedQueryBuilderStageTwo<TMappedResult> MapFailedInputTo<TMappedResult>()
        {
            return new MappedQueryBuilderStageTwo<TMappedResult>(_success);
        }

        public QueryBuilderStageThree<TInputModel, TResult> OnFailureBuildMappedResultWith<TMapFrom>(Func<TMapFrom, ActionResult> onFailure)
        {
            return new QueryBuilderStageThree<TInputModel, TResult>(_success,
                (input, mapper, error) => onFailure(mapper.Map<TInputModel, TMapFrom>(input)));
        }

        public QueryBuilderStageThree<TInputModel, TResult> OnFailureBuildResultWith(Func<TInputModel, ActionResult> onFailure)
        {
            return new QueryBuilderStageThree<TInputModel, TResult>(_success,
                (input, mapper, error) => onFailure(input));
        }

        public QueryBuilderStageThree<TInputModel, TResult> OnFailureHandleErrorWith(Func<ErrorContext, ActionResult> onFailure)
        {
            return new QueryBuilderStageThree<TInputModel, TResult>(_success,
                (input, mapper, error) => onFailure(error));
        }

        public QueryBuilderStageThree<TInputModel, TResult> OnFailureUseResultFrom(Func<ActionResult> onFailure)
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