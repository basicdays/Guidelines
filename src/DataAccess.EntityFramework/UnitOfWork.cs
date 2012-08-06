using Guidelines.Core;

namespace Guidelines.DataAccess.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
		private readonly IDbContext _dbContext;

		public UnitOfWork(IDbContext dbContext)
		{
			_dbContext = dbContext;
		}

    	public void Dispose()
    	{
			GetContext().Dispose();
    	}

    	public void Begin()
    	{
    		
    	}

    	public void Commit()
    	{
			GetContext().SaveChanges();
    	}

		public IDbContext GetContext()
		{
			return _dbContext;
		}

    	public void RollBack()
    	{

    	}
    }
}