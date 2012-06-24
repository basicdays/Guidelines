using System;
using Guidelines.Core;

namespace Guidelines.DataAccess.Mongo
{
	public class GuidMongoRepository<TDomain> : MongoRepository<TDomain, Guid?>
		where TDomain : IIdentifiable<Guid?>
	{
		public GuidMongoRepository(IMongoCollectionProvider<TDomain> collectionProvider) 
			: base(collectionProvider) {}
	}
}