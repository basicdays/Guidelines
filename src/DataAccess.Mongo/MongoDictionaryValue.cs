using System;

namespace Guidelines.DataAccess.Mongo
{
	[Obsolete("Use BsonDictionaryOptions instead.")]
	public class MongoDictionaryValue<TKey, TValue>
	{
		public TKey Key { get; set; }
		public TValue Value { get; set; }
	}
}
