using MongoDB.Driver;

namespace Guidelines.DataAccess.Mongo
{
	public interface IMongoCredentialProvider
	{
		MongoCredentials GetApplicationCredentials();
		string UserName { get; }
		string Password { get; }
		string DataBaseName { get; }
	}
}
