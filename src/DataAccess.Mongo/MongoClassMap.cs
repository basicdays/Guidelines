using System;
using MongoDB.Bson.Serialization;

namespace Guidelines.DataAccess.Mongo
{
	public abstract class MongoClassMap<TClass> : IMongoClassMap
	{
		protected abstract void Register(BsonClassMap<TClass> map);

		public void RegisterClassMaps()
		{
			Action<BsonClassMap<TClass>> registerAction = Register;

			BsonClassMap.RegisterClassMap(registerAction);
		}
	}
}
