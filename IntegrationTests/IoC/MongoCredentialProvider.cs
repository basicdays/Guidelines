using System;
using Guidelines.DataAccess.Mongo;
using MongoDB.Driver;

namespace Guidelines.IntegrationTests.IoC
{
	public class MongoCredentialProvider : IMongoCredentialProvider
	{
		private const string DataBase = "GuidelinesTest";

		public string UserName
		{
			get { return string.Empty; }
		}

		public string Password
		{
			get { return string.Empty; }
		}

		public string DataBaseName
		{
			get { return DataBase; }
		}

		public MongoCredentials GetApplicationCredentials()
		{
			//return new MongoCredentials(UserName, Password);
			return null;
		}
	}
}
