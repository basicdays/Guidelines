using System;

namespace Guidelines.Core.Commands
{
	public interface ICreateCommand<TDomain> : ICreateCommand<TDomain, Guid?>
		where TDomain : EntityBase<TDomain>
	{ }

	public interface ICreateCommand<TDomain, out TId>
		where TDomain : IIdentifiable<TId>
	{ }
}