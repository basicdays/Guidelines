using System;

namespace Guidelines.Core
{
	public interface IIdPolicy
	{
		Guid GetId();

		TEntity SetId<TEntity>(TEntity entity)
			where TEntity : EntityBase<TEntity>;
	}

	public interface IGuidIdGenerator<TDomain> : IIdGenerator<TDomain, Guid?>
		where TDomain : EntityBase<TDomain>
	{
		
	}

	public interface IIdGenerator<TDomain, out TId>
		where TDomain : IIdentifiable<TId>
	{
		TId GenerateId();
	}
}
