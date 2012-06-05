namespace Guidelines.DataAccess.Mongo
{
	/// <summary>
	/// This gives you an interface to setup a configuration through a class instead of the static gateway.
	/// </summary>
	public interface IMongoClassMap
	{
		void RegisterClassMaps();
	}
}