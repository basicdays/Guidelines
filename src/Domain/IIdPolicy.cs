using System;

namespace Guidelines.Core
{
	public interface IIdPolicy
	{
		Guid GetId();

		TEntity SetId<TEntity>(TEntity entity)
			where TEntity : EntityBase<TEntity>;
	}
}
