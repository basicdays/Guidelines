using System;

namespace Guidelines.Core.Commands
{
	public interface IUpdateCommand<TDomain> : IUpdateCommand<TDomain, Guid?>
		where TDomain : EntityBase<TDomain>
	{
		
	}

	public interface IUpdateCommand<TDomain, out TId>
		where TDomain : IIdentifiable<TId>
	{
		TId Id { get; }
	}
}