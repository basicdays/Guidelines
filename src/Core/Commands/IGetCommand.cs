using System;

namespace Guidelines.Core.Commands
{
	public interface IGetCommand<TDomain>
	{
		Guid Id { get; }
	}
}