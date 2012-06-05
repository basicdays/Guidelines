using System;
using System.Collections.Generic;
using System.Security;
using Guidelines.Domain.Properties;
using Guidelines.Domain.Specifications;
using Guidelines.Domain.Validation;

namespace Guidelines.Domain.Commands
{
	public interface IUpdateCommand<TDomain>
	{
		Guid Id { get; }
	}

	public interface IUpdateCommandHandler<in TCommand, TDomain>
	{
		TDomain Update(TCommand command, TDomain workOn);
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

		public UpdateCommandHandler(IRepository<TDomain> repository, IValidationEngine validationEngine, IEnumerable<IPermision<TDomain>> permisionSet, IUpdateCommandHandler<TUpdateCommand, TDomain> updater, IEnumerable<ICommandPermision<TUpdateCommand, TDomain>> commandPermisions)
		{
			_repository = repository;
			_commandPermisions = commandPermisions;
			_updater = updater;
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