using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Guidelines.Core.Properties;
using Guidelines.Core.Specifications;
using Guidelines.Core.Validation;

namespace Guidelines.Core.Commands
{
	public class CreateCommandHandler<TCreateCommand, TDomain, TId> : IQueryHandler<TCreateCommand, TDomain>, IRegisterMappings
		where TCreateCommand : ICreateCommand<TDomain, TId>
		where TDomain : class, IIdentifiable<TId>
	{
		private readonly IRepository<TDomain, TId> _repository;
		private readonly IValidationEngine _validationEngine;
		private readonly IEnumerable<IPermision<TDomain>> _permisionSet;
		private readonly IEnumerable<ICommandPermision<TCreateCommand, TDomain>> _commandPermisions;
		private readonly ICreateCommandHandler<TCreateCommand, TDomain> _creator;

		private readonly IEnumerable<ICommandAction<TCreateCommand, TDomain>> _preCommitActions;

		public CreateCommandHandler(IRepository<TDomain, TId> repository, IValidationEngine validationEngine, IEnumerable<IPermision<TDomain>> permisionSet, ICreateHandlerFactory<TCreateCommand, TDomain> creator, IEnumerable<ICommandPermision<TCreateCommand, TDomain>> commandPermisions, IEnumerable<ICommandAction<TCreateCommand, TDomain>> preCommitActions)
		{
			_repository = repository;
			_preCommitActions = preCommitActions;
			_commandPermisions = commandPermisions;
			_creator = creator.BuildCreator();
			_permisionSet = permisionSet;
			_validationEngine = validationEngine;
		}

		public TDomain Execute(TCreateCommand commandMessage)
		{
			TDomain entity = _creator.Create(commandMessage);

			bool wasCreatedSuccsfully = new Exists<TDomain>()
				.And(new IsAccessibleWith<TDomain>(_permisionSet))
				.And(new CanRunWithCommand<TCreateCommand, TDomain>(commandMessage, _commandPermisions))
				.IsSatisfiedBy(entity);

			if (!wasCreatedSuccsfully)
			{
				throw new SecurityException(Resources.Error_AccessDenied);
			}

			entity = _preCommitActions.Aggregate(entity, (current, preCommitAction) => preCommitAction.Execute(commandMessage, current));

			_validationEngine.Validate(entity);
			_repository.Insert(entity);

			return entity;
		}

		public Type SourceType
		{
			get { return typeof (TCreateCommand); }
		}

		public Type IdType
		{
			get { return typeof(TId); }
		}

		public Type DestinationType
		{
			get { return typeof (TDomain); }
		}

		public KeyGenerationMethod KeyGenerationMethod
		{
			get { return KeyGenerationMethod.Default; }
		}
	}
}