using MongoDB.Driver;

namespace Guidelines.DataAccess.Mongo
{
	public abstract class BaseCollectionProvider<TCollection> : IMongoCollectionProvider<TCollection>
	{
		private readonly IMongoDataSource _dataSource;
		private readonly string _collectionName;

		protected BaseCollectionProvider(IMongoDataSource dataSource, string collectionName)
		{
			_dataSource = dataSource;
			_collectionName = collectionName;
		}

		public MongoCollection<TCollection> GetCollection()
		{
			return _dataSource.GetCollection<TCollection>(_collectionName);
		}
	}
}
