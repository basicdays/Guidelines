using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Guidelines.DataAccess.EntityFramework
{
	public interface IDbContext 
	{
	    IDbSet<TEntity> Set<TEntity>() 
			where TEntity : class;

	    int SaveChanges();
		void Dispose();

		DbEntityEntry Entry(object toUpdate);
		DbEntityEntry<TEntity> Entry<TEntity>(TEntity toUpdate) 
			where TEntity : class;
	}
}
