namespace Guidelines.DataAccess.Mongo
{
	public interface IMongoConfigProvider
	{
		string GetConnectionString(string appEnvironment);
	}
}
