using System.Collections.Generic;
using Guidelines.Core;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Guidelines.DataAccess.Mongo
{
	public class MongoRepository<TDomain, TId> : IRepository<TDomain, TId>
		where TDomain : IIdentifiable<TId>
	{
		private readonly MongoCollection<TDomain> _collection;

		public MongoRepository(IMongoCollectionProvider<TDomain> collectionProvider)
		{
			_collection = collectionProvider.GetCollection();
		}

		public TDomain GetById(TId id)
		{
			return _collection.FindOneById(BsonValue.Create(id));
		}

		public IEnumerable<TDomain> GetAll()
		{
			return _collection.FindAll();
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
			var id = BsonValue.Create(toDelete.Id);
			_collection.Remove(Query.EQ("_id", id));
		}
	}
}
