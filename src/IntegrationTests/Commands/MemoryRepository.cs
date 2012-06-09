using System;
using System.Collections.Generic;
using Guidelines.Core;

namespace Guidelines.IntegrationTests.Commands
{
	public class MemoryRepository<TDomain> : IRepository<TDomain>
		where TDomain : EntityBase<TDomain>
	{
		public static Dictionary<Guid, TDomain> _memoryCache = new Dictionary<Guid, TDomain>(); 

		public TDomain GetById(Guid id)
		{
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
			if(toInsert.Id.HasValue) {
				_memoryCache.Add(toInsert.Id.Value, toInsert);
			}
			
			return toInsert;
		}

		public TDomain Update(TDomain toUpdate)
		{
			if(toUpdate.Id.HasValue && _memoryCache.ContainsKey(toUpdate.Id.Value)) {
				_memoryCache[toUpdate.Id.Value] = toUpdate;
			}

			return toUpdate;
		}

		public void Delete(TDomain toDelete)
		{
			if (toDelete.Id.HasValue) {
				_memoryCache.Remove(toDelete.Id.Value);
			}
		}

		public void Clear()
		{
			_memoryCache.Clear();
		}
	}
}
