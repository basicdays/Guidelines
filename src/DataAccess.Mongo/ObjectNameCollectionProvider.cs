namespace Guidelines.DataAccess.Mongo
{
	public class ObjectNameCollectionProvider<TCollection> : BaseCollectionProvider<TCollection>
	{
		public ObjectNameCollectionProvider(IMongoDataSource dataSource) 
			: base(dataSource, typeof(TCollection).Name)
		{ }
	}
}
