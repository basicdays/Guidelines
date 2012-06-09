using MongoDB.Driver;

namespace Guidelines.DataAccess.Mongo
{
	public interface IMongoCredentialProvider
	{
		MongoCredentials GetApplicationCredentials();
		string DataBaseName { get; }
	}
}
