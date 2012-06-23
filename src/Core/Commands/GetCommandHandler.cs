using System.Collections.Generic;
using System.Security;
using Guidelines.Core.Properties;
using Guidelines.Core.Specifications;

namespace Guidelines.Core.Commands
{
	public class GetCommandHandler<TGetCommand, TDomain, TId> : IQueryHandler<TGetCommand, TDomain>
		where TGetCommand : IGetCommand<TDomain, TId>
		where TDomain : class, IIdentifiable<TId>
	{
		private readonly IRepository<TDomain, TId> _repository;
		private readonly IEnumerable<IPermision<TDomain>> _permisionSet;
		private readonly IEnumerable<ICommandPermision<TGetCommand, TDomain>> _commandPermisions;

		public GetCommandHandler(IRepository<TDomain, TId> repository, IEnumerable<IPermision<TDomain>> permisionSet, IEnumerable<ICommandPermision<TGetCommand, TDomain>> commandPermisions)
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