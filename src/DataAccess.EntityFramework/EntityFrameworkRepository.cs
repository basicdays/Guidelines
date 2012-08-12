using System.Data.Entity;
using System.Linq;
using Guidelines.Core;

namespace Guidelines.DataAccess.EntityFramework
{
	public class EntityFrameworkRepository<TDomain> : IRepository<TDomain>
		where TDomain : class 
	{
		private readonly IDbContext _dbContext;
		private readonly IDbSet<TDomain> _set;

		protected EntityFrameworkRepository(IDbContext dbContext)
		{
			_dbContext = dbContext;
			_set = dbContext.Set<TDomain>();
		}

		protected IDbContext Context
		{
			get { return _dbContext; }
		}

		protected IDbSet<TDomain> Set
		{
			get { return _set; }
		}

		public IQueryable<TDomain> GetQueryableSet()
		{
			return Set;
		}

		public TDomain Insert(TDomain toInsert)
		{
			return Set.Add(toInsert);
		}

		public TDomain Update(TDomain toUpdate)
		{
			//EF tracks this for us.
			return toUpdate;
		}

		public void Delete(TDomain toDelete)
		{
			Set.Remove(toDelete);
		}
	}

	public class EntityFrameworkRepository<TDomain, TId> : EntityFrameworkRepository<TDomain>, IRepository<TDomain, TId>
		where TDomain : class, IIdentifiable<TId>
	{
		protected EntityFrameworkRepository(IDbContext dbContext) 
			: base(dbContext) {}

		public TDomain GetById(TId id)
		{
			return Set.Find(id);
		}
	}
}
