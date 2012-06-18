using MongoDB.Bson;
using MongoDB.Driver;

namespace Guidelines.DataAccess.Mongo
{
	public interface IMongoServerProvider
	{
		///<summary>
		/// Gets a reference to a <see cref="MongoCollection{T}"/> for the given <c>database</c> and
		/// <c>collectionName</c> using authentication information given by <c>credentials</c>
		///</summary>
		///<param name="database">name of the database</param>
		///<param name="collectionName">name of the collection</param>
		///<param name="credentials">credentials needed to connect to the database</param>
		///<typeparam name="T">type of an individual item in the collection</typeparam>
		///<returns>a reference to the collection</returns>
		MongoCollection<T> GetCollection<T>(string database, string collectionName, MongoCredentials credentials);

		///<summary>
		/// Gets a reference to a <see cref="MongoCollection{T}"/> for the given <c>database</c> and
		/// <c>collectionName</c> using given by <c>username</c> and <c>password</c>
		///</summary>
		///<param name="database">The name of the database</param>
		///<param name="collectionName">The name of the collection</param>
		///<param name="username">The username for authenticating</param>
		///<param name="password">The password for authenticating</param>
		///<typeparam name="T">Type of the items in the collection</typeparam>
		///<returns>A reference to the collection</returns>
		MongoCollection<T> GetCollection<T>(string database, string collectionName, string username,
														  string password);

		///<summary>
		/// Gets a reference to a <see cref="MongoCollection"/> for the given <c>database</c> and
		/// <c>collectionName</c> using given by <c>username</c> and <c>password</c>
		///</summary>
		///<param name="database">name of the database</param>
		///<param name="collectionName">name of the collection</param>
		///<param name="username">username for authenticating</param>
		///<param name="password">password for authenticating</param>
		///<returns>a reference to the collection in bson</returns>
		MongoCollection<BsonDocument> GetCollection(string database, string collectionName, string username,
													string password);

		///<summary>
		/// Gets a reference to a <see cref="MongoCollection"/> for the given <c>database</c> and
		/// <c>collectionName</c> using authentication information given by <c>credentials</c>
		///</summary>
		///<param name="database">The name of the database</param>
		///<param name="collectionName">The name of the collection</param>
		///<param name="credentials">The credentials needed to connect to the database</param>
		///<returns>A reference to the collection in bson</returns>
		MongoCollection<BsonDocument> GetCollection(string database, string collectionName, MongoCredentials credentials);

		/// <summary>
		/// Gets a reference to a <see cref="MongoDatabase"/> with authentication
		/// </summary>
		/// <param name="database">The database.</param>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		MongoDatabase GetDatabase(string database, string username, string password);

		/// <summary>
		/// Gets a reference to a <see cref="MongoDatabase"/> for guest
		/// </summary>
		/// <param name="database">The database.</param>
		/// <returns></returns>
		MongoDatabase GetDatabase(string DatabaseName);
	}
}
