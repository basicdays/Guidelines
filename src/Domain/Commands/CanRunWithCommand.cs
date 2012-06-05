using System.Collections.Generic;
using System.Linq;
using Guidelines.Domain.Specifications;

namespace Guidelines.Domain.Commands
{
	public class CanRunWithCommand<TCommand, TDomain> : ISpecification<TDomain>
	{
		private readonly IEnumerable<ICommandPermision<TCommand, TDomain>> _permisionSet;
		private readonly TCommand _command;

		public CanRunWithCommand(TCommand command, IEnumerable<ICommandPermision<TCommand, TDomain>> permisionSet)
		{
			_command = command;
			_permisionSet = permisionSet;
		}

		public bool IsSatisfiedBy(TDomain entity)
		{
			return _permisionSet.All(permision => permision.CanWorkOn(entity, _command));
		}
	}
}