using System.Collections.Generic;
using System.Linq;
using System.Security;
using Guidelines.Core.Properties;
using Guidelines.Core.Specifications;

namespace Guidelines.Core.Commands
{
	public class DeleteCommandHandler<TDeleteCommand, TDomain, TId> : ICommandHandler<TDeleteCommand>
		where TDeleteCommand : IDeleteCommand<TDomain, TId>
		where TDomain : class, IIdentifiable<TId>
	{
		private readonly IRepository<TDomain, TId> _repository;
		private readonly IEnumerable<IPermision<TDomain>> _permisionSet;
		private readonly IEnumerable<ICommandPermision<TDeleteCommand, TDomain>> _commandPermisions;

		private readonly IEnumerable<ICommandAction<TDeleteCommand, TDomain>> _preCommitActions;

		public DeleteCommandHandler(IRepository<TDomain, TId> repository, IEnumerable<IPermision<TDomain>> permisionSet, IEnumerable<ICommandPermision<TDeleteCommand, TDomain>> commandPermisions, IEnumerable<ICommandAction<TDeleteCommand, TDomain>> preCommitActions)
		{
			_repository = repository;
			_preCommitActions = preCommitActions;
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

			entity = _preCommitActions.Aggregate(entity, (current, preCommitAction) => preCommitAction.Execute(commandMessage, current));

			_repository.Delete(entity);
		}
	}
}