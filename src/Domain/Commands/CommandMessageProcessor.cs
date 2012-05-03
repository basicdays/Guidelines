using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Security;
using Guidelines.Domain.Validation;
using log4net;

namespace Guidelines.Domain.Commands
{
	public class CommandMessageProcessor : ICommandMessageProcessor
	{
		private readonly ILog _logger;
		private readonly IEnumerable<ICommandPreprocessor> _commandPreporcesors;

		public CommandMessageProcessor(ILog logger, IEnumerable<ICommandPreprocessor> commandPreporcesors)
		{
			_logger = logger;
			_commandPreporcesors = commandPreporcesors;
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
			Exception errors = null;

			try {
				IEnumerable<ICommandPreprocessor> eligiblePreprocessors = 
					_commandPreporcesors.Where(preprocessor => preprocessor.CommandIsEligible(commandMessage));

				foreach (ICommandPreprocessor preprocessor in eligiblePreprocessors) {
					preprocessor.PreprocessCommand(commandMessage);
				}

				toExecute(commandMessage);
			}
			catch (Exception e)
			{
				errors = AddError(e);
			}

			return errors;
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
				_logger.Debug(e.Message, e);
			}
			else
			{
				_logger.Error(e.Message, e);
			}

			return e;
		}
	}
}
