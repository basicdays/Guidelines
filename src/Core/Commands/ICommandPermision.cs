using System;

namespace Guidelines.Core.Commands
{
	public interface ICommandPermision<in TCommand, in TDomain>
	{
		bool CanWorkOn(TDomain entity, TCommand command);
	}
}