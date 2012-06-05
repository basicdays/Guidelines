using MongoDB.Bson;
using MongoDB.Driver;

namespace Guidelines.DataAccess.Mongo
{
	public interface IMongoDataSource
	{
		MongoCollection<T> GetCollection<T>(string collectionName);
		MongoCollection<BsonDocument> GetCollection(string collectionName);
		MongoDatabase GetDatabase();
	}
}
