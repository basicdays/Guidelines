using System;
using System.Collections.Generic;
using System.Security;
using Guidelines.Core.Properties;
using Guidelines.Core.Specifications;

namespace Guidelines.Core.Commands
{
	public interface IDeleteCommand<TDomain>
	{
		Guid Id { get; }
	}

	public interface IDeleteCommandHandler<TDomain>
	{
		TDomain Update(TDomain workOn);
	}

	public class DeleteCommandHandler<TDeleteCommand, TDomain> : ICommandHandler<TDeleteCommand>
		where TDeleteCommand : IDeleteCommand<TDomain>
		where TDomain : class
	{
		private readonly IRepository<TDomain> _repository;
		private readonly IEnumerable<IPermision<TDomain>> _permisionSet;
		private readonly IEnumerable<ICommandPermision<TDeleteCommand, TDomain>> _commandPermisions;

		public DeleteCommandHandler(IRepository<TDomain> repository, IEnumerable<IPermision<TDomain>> permisionSet, IEnumerable<ICommandPermision<TDeleteCommand, TDomain>> commandPermisions)
		{
			_repository = repository;
			_commandPermisions = commandPermisions;
			_permisionSet = permisionSet;
		}

		public void Execute(TDeleteCommand commandMessage)
		{
			TDomain entity = _repository.GetById(commandMessage.Id);

			bool isModifiable = new Exists<TDomain>()
				.And(new IsAccessibleWith<TDomain>(_permisionSet))
				.And(new CanRunWithCommand<TDeleteCommand, TDomain>(commandMessage, _commandPermisions))
				.IsSatisfiedBy(entity);

			if (!isModifiable)
			{
				throw new SecurityException(Resources.Error_AccessDenied);
			}

			_repository.Delete(entity);
		}
	}
}