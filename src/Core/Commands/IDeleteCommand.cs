using System;

namespace Guidelines.Core.Commands
{
	public interface IDeleteCommand<TDomain> : IDeleteCommand<TDomain, Guid?>
		where TDomain : EntityBase<TDomain>
	{

	}

	public interface IDeleteCommand<TDomain, out TId>
		where TDomain : IIdentifiable<TId>
	{
		TId Id { get; }
	}
}