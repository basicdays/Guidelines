using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Guidelines.Core.Properties;
using Guidelines.Core.Specifications;
using Guidelines.Core.Validation;

namespace Guidelines.Core.Commands
{
	public interface IUpdateCommand<TDomain>
	{
		Guid Id { get; }
	}

	public interface IUpdateCommandHandler<in TCommand, TDomain>
	{
		TDomain Update(TCommand command, TDomain workOn);
	}

	public interface IUpdateHandlerFactory<in TCommand, TDomain> {
		IUpdateCommandHandler<TCommand, TDomain> BuildUpdater();
	}

	public class UpdateHandlerFactory<TCommand, TDomain> : IUpdateHandlerFactory<TCommand, TDomain>
	{
		private readonly IEnumerable<IUpdateCommandHandler<TCommand, TDomain>> _updater;

		public UpdateHandlerFactory(IEnumerable<IUpdateCommandHandler<TCommand, TDomain>> updater)
		{
			_updater = updater;
		}

		public IUpdateCommandHandler<TCommand, TDomain> BuildUpdater()
		{
			return _updater.Count() > 1 
				? _updater.FirstOrDefault(updater => !(updater is DefaultMappingUpdater<TCommand, TDomain>)) 
				: _updater.FirstOrDefault();
		}
	}

	public class UpdateCommandHandler<TUpdateCommand, TDomain> : ICommandHandler<TUpdateCommand>
		where TUpdateCommand : IUpdateCommand<TDomain>
		where TDomain : class
	{
		private readonly IRepository<TDomain> _repository;
		private readonly IValidationEngine _validationEngine;
		private readonly IEnumerable<IPermision<TDomain>> _permisionSet;
		private readonly IEnumerable<ICommandPermision<TUpdateCommand, TDomain>> _commandPermisions;
		private readonly IUpdateCommandHandler<TUpdateCommand, TDomain> _updater;

		public UpdateCommandHandler(IRepository<TDomain> repository, IValidationEngine validationEngine, IEnumerable<IPermision<TDomain>> permisionSet, IUpdateHandlerFactory<TUpdateCommand, TDomain> updaterFactory, IEnumerable<ICommandPermision<TUpdateCommand, TDomain>> commandPermisions)
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
	}

	public class DefaultMappingUpdater<TCommand, TDomain> : IUpdateCommandHandler<TCommand, TDomain>
	{
		private readonly IMapper<TCommand, TDomain> _mapper;

		public DefaultMappingUpdater(IMapper<TCommand, TDomain> mapper)
		{
			_mapper = mapper;
		}

		public TDomain Update(TCommand command, TDomain workOn)
		{
			return _mapper.Map(command, workOn);
		}
	}
}