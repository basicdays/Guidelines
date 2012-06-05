using System;
using System.Collections.Generic;
using System.Security;
using Guidelines.Domain.Properties;
using Guidelines.Domain.Specifications;

namespace Guidelines.Domain.Commands
{
	public interface IGetCommand<TDomain>
	{
		Guid Id { get; }
	}

	public class GetCommandHandler<TGetCommand, TDomain> : IQueryHandler<TGetCommand, TDomain>
		where TGetCommand : IGetCommand<TDomain>
		where TDomain : class
	{
		private readonly IRepository<TDomain> _repository;
		private readonly IEnumerable<IPermision<TDomain>> _permisionSet;
		private readonly IEnumerable<ICommandPermision<TGetCommand, TDomain>> _commandPermisions;

		public GetCommandHandler(IRepository<TDomain> repository, IEnumerable<IPermision<TDomain>> permisionSet, IEnumerable<ICommandPermision<TGetCommand, TDomain>> commandPermisions)
		{
			_repository = repository;
			_commandPermisions = commandPermisions;
			_permisionSet = permisionSet;
		}

		public TDomain Execute(TGetCommand commandMessage)
		{
			TDomain entity = _repository.GetById(commandMessage.Id);

			bool isAccessible = new Exists<TDomain>()
				.And(new IsAccessibleWith<TDomain>(_permisionSet))
				.And(new CanRunWithCommand<TGetCommand, TDomain>(commandMessage, _commandPermisions))
				.IsSatisfiedBy(entity);

			if (!isAccessible)
			{
				throw new SecurityException(Resources.Error_AccessDenied);
			}

			return entity;
		}
	}
}