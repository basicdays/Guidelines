using System;

namespace Guidelines.Domain
{
	public interface IIdPolicy
	{
		Guid GetId();

		TEntity SetId<TEntity>(TEntity entity)
			where TEntity : EntityBase<TEntity>;
	}
}
