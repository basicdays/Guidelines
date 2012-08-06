using System;
using Guidelines.Core;

namespace Guidelines.DataAccess.EntityFramework
{
	public class GuidEntityFrameworkRepository<TDomain> : EntityFrameworkRepositoryBase<TDomain, Guid?>
		where TDomain : class, IIdentifiable<Guid?>
	{
		protected GuidEntityFrameworkRepository(IDbContext dbContext) : base(dbContext) {}
	}
}
