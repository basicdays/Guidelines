using System.Linq;
using Guidelines.Core;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Guidelines.DataAccess.Mongo
{
	public class MongoRepository<TDomain, TId> : IRepository<TDomain, TId>, IQueryableRepository<TDomain>
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

		public IQueryable<TDomain> GetQueryableSet()
		{
			return _collection.AsQueryable();
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

		public long Delete(TDomain toDelete)
		{
			var id = BsonValue.Create(toDelete.Id);
			var result = _collection.Remove(Query.EQ("_id", id));
			return result.DocumentsAffected;
		}
	}
}
