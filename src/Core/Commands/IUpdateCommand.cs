using System;

namespace Guidelines.Core.Commands
{
	public interface IUpdateCommand<TDomain>
	{
		Guid Id { get; }
	}
}