using System;
using System.Collections.Generic;
using System.Security;
using Guidelines.Core.Properties;
using Guidelines.Core.Specifications;
using Guidelines.Core.Validation;

namespace Guidelines.Core.Commands
{
	public class UpdateCommandHandler<TUpdateCommand, TDomain, TId> : ICommandHandler<TUpdateCommand>, IRegisterMappings
		where TUpdateCommand : IUpdateCommand<TDomain, TId>
		where TDomain : class, IIdentifiable<TId>
	{
		private readonly IRepository<TDomain, TId> _repository;
		private readonly IValidationEngine _validationEngine;
		private readonly IEnumerable<IPermision<TDomain>> _permisionSet;
		private readonly IEnumerable<ICommandPermision<TUpdateCommand, TDomain>> _commandPermisions;
		private readonly IUpdateCommandHandler<TUpdateCommand, TDomain> _updater;

		public UpdateCommandHandler(IRepository<TDomain, TId> repository, IValidationEngine validationEngine, IEnumerable<IPermision<TDomain>> permisionSet, IUpdateHandlerFactory<TUpdateCommand, TDomain> updaterFactory, IEnumerable<ICommandPermision<TUpdateCommand, TDomain>> commandPermisions)
		{
			_repository = repository;
			_commandPermisions = commandPermisions;
			_updater = updaterFactory.BuildUpdater();
			_permisionSet = permisionSet;
			_validationEngine = validationEngine;
		}

		public void Execute(TUpdateCommand commandMessage)
		{
			TDomain entity = _repository.GetById(commandMessage.Id);

			bool isModifiable = new Exists<TDomain>()
				.And(new IsAccessibleWith<TDomain>(_permisionSet))
				.And(new CanRunWithCommand<TUpdateCommand, TDomain>(commandMessage, _commandPermisions))
				.IsSatisfiedBy(entity);

			if (!isModifiable)
			{
				throw new SecurityException(Resources.Error_AccessDenied);
			}

			TDomain updatedEntity = _updater.Update(commandMessage, entity);

			_validationEngine.Validate(updatedEntity);
			_repository.Update(updatedEntity);
		}

		public Type SourceType
		{
			get { return typeof(TUpdateCommand); }
		}

		public Type DestinationType
		{
			get { return typeof(TDomain); }
		}

		public Type IdType
		{
			get { return typeof(TId); }
		}

		public KeyGenerationMethod KeyGenerationMethod
		{
			get { return KeyGenerationMethod.None; }
		}
	}
}