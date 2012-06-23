using System;
using System.Collections.Generic;
using Guidelines.Core;

namespace Guidelines.IntegrationTests.Commands
{
	public class MemoryRepository<TDomain> : MemoryRepository<TDomain, Guid?> 
		where TDomain : IIdentifiable<Guid?>
	{
		public override Guid? GenerateId()
		{
			return Guid.NewGuid();
		}
	}

	public class MemoryRepository<TDomain, TId> : IRepository<TDomain, TId>, IIdGenerator<TDomain, TId>
		where TDomain : IIdentifiable<TId>
	{
		public static Dictionary<TId, TDomain> _memoryCache = new Dictionary<TId, TDomain>();

		public TDomain GetById(TId id)
		{
			if (id == null)
				return default(TDomain);

			TDomain value;
			_memoryCache.TryGetValue(id, out value);

			return value;
		}

		public IEnumerable<TDomain> GetAll()
		{
			return _memoryCache.Values;
		}

		public TDomain Insert(TDomain toInsert)
		{
			if(toInsert.Id != null) {
				_memoryCache.Add(toInsert.Id, toInsert);
			}
			
			return toInsert;
		}

		public TDomain Update(TDomain toUpdate)
		{
			if (toUpdate.Id != null && _memoryCache.ContainsKey(toUpdate.Id))
			{
				_memoryCache[toUpdate.Id] = toUpdate;
			}

			return toUpdate;
		}

		public void Delete(TDomain toDelete)
		{
			if (toDelete.Id != null)
			{
				_memoryCache.Remove(toDelete.Id);
			}
		}

		public void Clear()
		{
			_memoryCache.Clear();
		}

		public virtual TId GenerateId()
		{
			return default(TId);
		}
	}
}
