using MongoDB.Driver;

namespace Guidelines.DataAccess.Mongo
{
	public interface IMongoCollectionProvider<TCollection>
	{
		MongoCollection<TCollection> GetCollection();
	}
}
