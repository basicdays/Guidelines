﻿using System;
using System.Linq;

namespace Guidelines.Core
{
	public interface IRepository<TDomain>
	{
		TDomain Insert(TDomain toInsert);
		TDomain Update(TDomain toUpdate);
		long Delete(TDomain toDelete);
	}

	public interface IRepository<TDomain, in TId> : IRepository<TDomain>
		where TDomain : IIdentifiable<TId>
	{
		TDomain GetById(TId id);
	}

	public interface IQueryableRepository<TDomain> : IRepository<TDomain>
	{
		IQueryable<TDomain> GetQueryableSet();
	}
}