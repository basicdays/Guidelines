using System.Collections.Generic;
using System.Linq;
using System.Security;
using Guidelines.Core.Properties;
using Guidelines.Core.Specifications;
using Guidelines.Core.Validation;

namespace Guidelines.Core.Commands
{
	public interface ICreateCommand<TDomain>
	{ }

	public interface ICreateCommandHandler<in TCommand, out TDomain>
	{
		TDomain Create(TCommand command);
	}

	public interface ICreateHandlerFactory<in TCommand, out TDomain>
	{
		ICreateCommandHandler<TCommand, TDomain> BuildCreator();
	}

	public class CreateHandlerFactory<TCommand, TDomain> : ICreateHandlerFactory<TCommand, TDomain>
	{
		private readonly IEnumerable<ICreateCommandHandler<TCommand, TDomain>> _creators;

		public CreateHandlerFactory(IEnumerable<ICreateCommandHandler<TCommand, TDomain>> creator)
		{
			_creators = creator;
		}

		public ICreateCommandHandler<TCommand, TDomain> BuildCreator()
		{
			return _creators.Count() > 1
				? _creators.FirstOrDefault(updater => !(updater is DefaultMappingCreator<TCommand, TDomain>))
				: _creators.FirstOrDefault();
		}
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

		public CreateCommandHandler(IRepository<TDomain> repository, IValidationEngine validationEngine, IEnumerable<IPermision<TDomain>> permisionSet, ICreateHandlerFactory<TCreateCommand, TDomain> creator, IEnumerable<ICommandPermision<TCreateCommand, TDomain>> commandPermisions)
		{
			_repository = repository;
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