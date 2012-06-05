using System;

namespace Guidelines.Domain
{
	public interface IRepository<TDomain>
	{
		TDomain GetById(Guid id);
		TDomain Insert(TDomain toInsert);
		TDomain Update(TDomain toUpdate);
		void Delete(TDomain toDelete);
	}
}