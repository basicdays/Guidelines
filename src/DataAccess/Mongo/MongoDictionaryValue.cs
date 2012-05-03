namespace Guidelines.DataAccess.Mongo
{
	public class MongoDictionaryValue<TKey, TValue>
	{
		public TKey Key { get; set; }
		public TValue Value { get; set; }
	}
}
