using System;

namespace Guidelines.Core.Commands
{
	public interface IDeleteCommand<TDomain>
	{
		Guid Id { get; }
	}
}