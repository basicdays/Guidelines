using System;
using Guidelines.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Guidelines.DataAccess.Mongo
{
	public class MongoRepository<TDomain> : IRepository<TDomain>
		where TDomain : EntityBase<TDomain>
	{
		private readonly MongoCollection<TDomain> _collection;

		public MongoRepository(IMongoCollectionProvider<TDomain> collectionProvider)
		{
			_collection = collectionProvider.GetCollection();
		}

		public TDomain GetById(Guid id)
		{
			return _collection.FindOneById(id);
		}

		public TDomain Insert(TDomain toInsert)
		{
			_collection.Insert(toInsert);

			return toInsert;
		}

		public TDomain Update(TDomain toUpdate)
		{
			_collection.Save(toUpdate);

			return toUpdate;
		}

		public void Delete(TDomain toDelete)
		{
			_collection.Remove(Query.EQ("_id", toDelete.Id));
		}
	}
}
