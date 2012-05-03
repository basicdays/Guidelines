using System.Reflection;
using log4net;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Guidelines.DataAccess.Mongo
{
	//review: could we call this MongoServerProvider to match MongoServer?
	public class MongoConnectionProvider : IMongoConnectionProvider
	{
		private readonly IMongoConfigProvider _configurationProvider;
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public MongoConnectionProvider(IMongoConfigProvider configurationProvider)
		{
			_configurationProvider = configurationProvider;
		}

		/// <summary>
		/// Gets an instance of <see cref="MongoServer"/> using settings for the
		/// current AppEnvironment.Instance
		/// </summary>
		/// <returns></returns>
		public MongoServer Create()
		{
			return Create("local");//AppEnvironment.Instance.AppEnvironmentName);
		}

		/// <summary>
		/// Gets an instance of <see cref="MongoServer"/> using settings for the
		/// given AppEnvironmentName value.
		/// </summary>
		/// <param name="appEnvironment">
		/// Following environments are supported: <code>Development</code>, <code>Beta</code> & <code>Production</code>
		/// </param>
		/// <remarks>Value is not case-sensitive. Also supports *64 versions of the appEnvironment</remarks>        
		/// <returns></returns>
		public MongoServer Create(string appEnvironment)
		{
			var connectionString = _configurationProvider.GetConnectionString(appEnvironment);
			Log.DebugFormat("MongoDB Connection String: {0}", connectionString);

			return MongoServer.Create(connectionString);
		}

		///<summary>
		/// Gets a reference to a <see cref="MongoCollection{T}"/> for the given <c>database</c> and
		/// <c>collectionName</c> using authentication information given by <c>credentials</c>
		///</summary>
		///<param name="database">name of the database</param>
		///<param name="collectionName">name of the collection</param>
		///<param name="credentials">credentials needed to connect to the database</param>
		///<typeparam name="T">type of an individual item in the collection</typeparam>
		///<returns>a reference to the collection</returns>
		public MongoCollection<T> GetCollection<T>(string database, string collectionName, MongoCredentials credentials)
		{
			return Create().GetDatabase(database, credentials).GetCollection<T>(collectionName);
		}
		///<summary>
		/// Gets a reference to a <see cref="MongoCollection{T}"/> for the given <c>database</c> and
		/// <c>collectionName</c> using given by <c>username</c> and <c>password</c>
		///</summary>
		///<param name="database">name of the database</param>
		///<param name="collectionName">name of the collection</param>
		///<param name="username">username for authenticating</param>
		///<param name="password">password for authenticating</param>
		///<typeparam name="T">type of an individual item in the collection</typeparam>
		///<returns>a reference to the collection</returns>
		public MongoCollection<T> GetCollection<T>(string database, string collectionName, string username, string password)
		{
			return GetCollection<T>(database, collectionName, new MongoCredentials(username, password));
		}

		///<summary>
		/// Gets a reference to a <see cref="MongoCollection"/> for the given <c>database</c> and
		/// <c>collectionName</c> using given by <c>username</c> and <c>password</c>
		///</summary>
		///<param name="database">name of the database</param>
		///<param name="collectionName">name of the collection</param>
		///<param name="username">username for authenticating</param>
		///<param name="password">password for authenticating</param>
		///<returns>a reference to the collection</returns>
		public MongoCollection<BsonDocument> GetCollection(string database, string collectionName, string username, string password)
		{
			return GetCollection(database, collectionName, new MongoCredentials(username, password));
		}

		///<summary>
		/// Gets a reference to a <see cref="MongoCollection"/> for the given <c>database</c> and
		/// <c>collectionName</c> using authentication information given by <c>credentials</c>
		///</summary>
		///<param name="database">name of the database</param>
		///<param name="collectionName">name of the collection</param>
		///<param name="credentials">credentials needed to connect to the database</param>
		///<returns>a reference to the collection</returns>
		public MongoCollection<BsonDocument> GetCollection(string database, string collectionName, MongoCredentials credentials)
		{
			return Create().GetDatabase(database, credentials).GetCollection(collectionName);
		}

		/// <summary>
		/// Gets a reference to a <see cref="MongoDatabase"/>
		/// </summary>
		/// <param name="database">The database.</param>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		public MongoDatabase GetDatabase(string database, string username, string password)
		{
			return Create().GetDatabase(database, new MongoCredentials(username, password));
		}

		public MongoDatabase GetDatabase(string database)
		{
			return Create().GetDatabase(database);
		}
	}
}
