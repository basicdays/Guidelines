using System;

namespace Guidelines.Core
{
	public interface IRepository<TDomain>
	{
		TDomain GetById(Guid id);
		TDomain Insert(TDomain toInsert);
		TDomain Update(TDomain toUpdate);
		void Delete(TDomain toDelete);
	}
}