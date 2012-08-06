using System;
using System.Linq;

namespace Guidelines.Core
{
	public interface IRepository<TDomain, in TId>
		where TDomain : IIdentifiable<TId>
	{
		TDomain GetById(TId id);
		IQueryable<TDomain> GetQueryableSet();
		TDomain Insert(TDomain toInsert);
		TDomain Update(TDomain toUpdate);
		void Delete(TDomain toDelete);
	}
}