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
        public CommandBuilderStageTwo<TInputModel> OnSuccessBuildResultWith(Func<ActionResult> onSuccess)
        {
            return new CommandBuilderStageTwo<TInputModel>(onSuccess);
        }

        public CommandBuilderStageTwo<TInputModel> OnSuccessUseResult(ActionResult actionResult)
        {
            return new CommandBuilderStageTwo<TInputModel>(() => actionResult);
        }
    }

    public class CommandBuilderStageTwo<TInputModel>
    {
        public class MappedCommandBuilderStageTwo<TMapTo>
        {
            private readonly Func<ActionResult> _mappedSuccess;

            public MappedCommandBuilderStageTwo(Func<ActionResult> success)
            {
                _mappedSuccess = success;
            }

            public CommandBuilderStageThree<TInputModel> OnFailureBuildResultWith(Func<TMapTo, ActionResult> onFailure)
            {
                return new CommandBuilderStageThree<TInputModel>(_mappedSuccess,
                    (input, mapper, error) => onFailure(mapper.Map<TInputModel, TMapTo>(input)));
            }
        }

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

        public MappedCommandBuilderStageTwo<TMappedResult> MapFailedInputTo<TMappedResult>()
        {
            return new MappedCommandBuilderStageTwo<TMappedResult>(_success);
        }

        public CommandBuilderStageThree<TInputModel> OnFailureBuildMappedResultWith<TMapFrom>(Func<TMapFrom, ActionResult> onFailure)
        {
            return new CommandBuilderStageThree<TInputModel>(_success,
                (input, mapper, error) => onFailure(mapper.Map<TInputModel, TMapFrom>(input)));
        }

        public CommandBuilderStageThree<TInputModel> OnFailureBuildResultWith(Func<TInputModel, ActionResult> onFailure)
        {
            return new CommandBuilderStageThree<TInputModel>(_success,
            (input, mapper, error) => onFailure(input));
        }

		public CommandBuilderStageThree<TInputModel> OnFailureHandleErrorWith(Func<ErrorContext, ActionResult> onFailure)
        {
            return new CommandBuilderStageThree<TInputModel>(_success,
            (input, mapper, error) => onFailure(error));
        }

        public CommandBuilderStageThree<TInputModel> OnFailureUseResultFrom(Func<ActionResult> onFailure)
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