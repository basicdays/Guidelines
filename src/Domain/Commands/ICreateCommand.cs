using System;
using System.Collections.Generic;
using System.Security;
using Guidelines.Domain.Properties;
using Guidelines.Domain.Specifications;
using Guidelines.Domain.Validation;

namespace Guidelines.Domain.Commands
{
	public interface ICreateCommand<TDomain>
	{ }

	public interface ICreateCommandHandler<TCommand, TDomain>
	{
		TDomain Create(TCommand command);
	}

	public class CreateCommandHandler<TCreateCommand, TDomain> : IQueryHandler<TCreateCommand, TDomain>
		where TCreateCommand : ICreateCommand<TDomain>
		where TDomain : class
	{
		private readonly IRepository<TDomain> _repository;
		private readonly IValidationEngine _validationEngine;
		private readonly IEnumerable<IPermision<TDomain>> _permisionSet;
		private readonly IEnumerable<ICommandPermision<TCreateCommand, TDomain>> _commandPermisions;
		private readonly ICreateCommandHandler<TCreateCommand, TDomain> _creator;

		public CreateCommandHandler(IRepository<TDomain> repository, IValidationEngine validationEngine, IEnumerable<IPermision<TDomain>> permisionSet, ICreateCommandHandler<TCreateCommand, TDomain> creator, IEnumerable<ICommandPermision<TCreateCommand, TDomain>> commandPermisions)
		{
			_repository = repository;
			_commandPermisions = commandPermisions;
			_creator = creator;
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

			_validationEngine.Validate(entity);
			_repository.Insert(entity);

			return entity;
		}
	}

	public class DefaultMappingCreator<TCommand, TDomain> : ICreateCommandHandler<TCommand, TDomain>
	{
		private readonly IMapper<TCommand, TDomain> _mapper;

		public DefaultMappingCreator(IMapper<TCommand, TDomain> mapper)
		{
			_mapper = mapper;
		}

		public TDomain Create(TCommand command)
		{
			return _mapper.Map(command);
		}
	}
}