using MongoDB.Bson;
using MongoDB.Driver;

namespace Guidelines.DataAccess.Mongo
{
	public class MongoDataSource : IMongoDataSource
	{
		private readonly IMongoServerProvider _dataSource;
		private readonly IMongoCredentialProvider _credentials;

		public MongoDataSource(IMongoServerProvider dataSource, IMongoCredentialProvider credentials)
		{
			_dataSource = dataSource;
			_credentials = credentials;
		}

		protected string DatabaseName
		{
			get { return _credentials.DataBaseName; }
		}

		protected MongoCredentials Credentials
		{
			get { return _credentials.GetApplicationCredentials(); }
		}

		public MongoCollection<TCollection> GetCollection<TCollection>(string collectionName)
		{
			return _dataSource.GetCollection<TCollection>(DatabaseName, collectionName, Credentials);
		}

		public MongoCollection<BsonDocument> GetCollection(string collectionName)
		{
			return _dataSource.GetCollection(DatabaseName, collectionName, Credentials);
		}

		public MongoDatabase GetDatabase()
		{
			return Credentials != null 
				? _dataSource.GetDatabase(DatabaseName, Credentials.Username, Credentials.Password)
				: _dataSource.GetDatabase(DatabaseName);
		}
	}
}
