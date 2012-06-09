using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security;
using Guidelines.Core.Validation;
using log4net;

namespace Guidelines.Core.Commands
{
	public class CommandMessageProcessor : ICommandMessageProcessor
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILog _logger;
		private readonly IEnumerable<ICommandPreprocessor> _commandPreporcesors;
		private readonly IEnumerable<ICommitHook> _commitHooks;

		public CommandMessageProcessor(IUnitOfWork unitOfWork, ILog logger, IEnumerable<ICommandPreprocessor> commandPreporcesors, IEnumerable<ICommitHook> commitHooks)
		{
			_unitOfWork = unitOfWork;
			_commandPreporcesors = commandPreporcesors;
			_commitHooks = commitHooks;
			_logger = logger;
		}

		public QueryResult<TResult> Execute<TCommand, TResult>(TCommand commandMessage, IQueryHandler<TCommand, TResult> handler)
		{
			TResult result = default(TResult);

			var errors = Execute(commandMessage,
				delegate { result = handler.Execute(commandMessage); });

			return new QueryResult<TResult>(errors, result);
		}

		public CommandResult Execute<TCommand>(TCommand commandMessage, ICommandHandler<TCommand> handler)
		{
			var errors = Execute(commandMessage, delegate { handler.Execute(commandMessage); });

			return new CommandResult(errors);
		}

		private Exception Execute<TCommand>(TCommand commandMessage, Action<TCommand> toExecute)
		{
			_unitOfWork.Begin();

			Exception errors = null;
			bool success = false;

			try
			{
				TakeOnEligableCommand(_commandPreporcesors,
					preprocessor => preprocessor.CommandIsEligible(commandMessage),
					preprocessor => preprocessor.PreprocessCommand(commandMessage));

				toExecute(commandMessage);

				_unitOfWork.Commit();

				success = true;
			}
			catch (Exception e)
			{
				_unitOfWork.RollBack();

				errors = AddError(e);
			}
			finally
			{
				_unitOfWork.Dispose();
			}

			if (success)
			{
				TakeOnEligableCommand(_commitHooks,
					hook => hook.CommandIsEligible(commandMessage),
					hook => hook.OnSuccessfulCommit(commandMessage));
			}

			return errors;
		}

		private static void TakeOnEligableCommand<TService>(IEnumerable<TService> services, Func<TService, bool> isEligable, Action<TService> onSuccess)
		{
			IEnumerable<TService> eligibleServices = services.Where(isEligable);

			foreach (TService commitHook in eligibleServices)
			{
				onSuccess(commitHook);
			}
		}

		private Exception AddError(Exception e)
		{
			//TargetInvocationException just winds up wrapping the real error, so strip it off.
			if (e is TargetInvocationException && e.InnerException != null)
			{
				e = e.InnerException;
			}

			if (e is ValidationEngineException || e is SecurityException || e is ValidationException)
			{
				_logger.Warn(e.Message, e);
			}
			else
			{
				_logger.Error(e.Message, e);
			}

			return e;
		}
	}
}
