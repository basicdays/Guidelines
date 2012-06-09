using System;
using System.Collections.Generic;
using Guidelines.DataAccess.Mongo;

namespace Guidelines.IntegrationTests.IoC
{
	public class MongoConfigProvider : IMongoConfigProvider
	{
		//app settings at some point.
		private static readonly Dictionary<string, string> KnownConfigurations
			= new Dictionary<string, string> {
				{"local", "mongodb://localhost/?safe=true&guids=Standard"}
			};

		public string GetConnectionString(string appEnvironment)
		{
			var tmp = appEnvironment.Trim();

			string url;
			tmp = tmp.ToLower();
			if (KnownConfigurations.TryGetValue(tmp, out url)) {
				return url;
			}

			throw new InvalidOperationException(string.Format("{0} is an invalid application envrionment", appEnvironment));
		}
	}
}
