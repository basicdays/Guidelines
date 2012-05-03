using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Guidelines.DataAccess.Mongo
{
	//The default mongo dictionary serializer turns them into nested sets arrays for key value pairs.
	//This makes them unquerrable and unindexable to search off of.
	//This turns the dictionary into an object with a key and value so they can be search for and indexed.
	public class MongoDictionarySerializer<TKey, TValue> : IBsonSerializer
	{

		public object Deserialize(BsonReader bsonReader, Type nominalType, IBsonSerializationOptions options)
		{
			if (nominalType != typeof(Dictionary<TKey, TValue>))
			{
				throw new ArgumentException("Cannot serialize anything but self");
			}
			var ser = new ArraySerializer<MongoDictionaryValue<TKey, TValue>>();
			var nameEntries = (MongoDictionaryValue<TKey, TValue>[])ser.Deserialize(bsonReader, typeof(MongoDictionaryValue<TKey, TValue>[]), options) ?? new MongoDictionaryValue<TKey, TValue>[] { };

			return nameEntries.ToDictionary(nameLookupEntry => nameLookupEntry.Key, nameLookupEntry => nameLookupEntry.Value);
		}

		public object Deserialize(BsonReader bsonReader, Type nominalType, Type actualType, IBsonSerializationOptions options)
		{
			return Deserialize(bsonReader, nominalType, options);
		}

		public void Serialize(BsonWriter bsonWriter, Type nominalType, object value, IBsonSerializationOptions options)
		{
			if (nominalType != typeof(Dictionary<TKey, TValue>))
			{
				throw new ArgumentException("Cannot serialize anything but self");
			}
			var dictionary = value as Dictionary<TKey, TValue>;

			IEnumerable<MongoDictionaryValue<TKey, TValue>> databaseEntries;
			if (dictionary != null)
			{
				databaseEntries = dictionary.Select(keyValuePair =>
					new MongoDictionaryValue<TKey, TValue>
						{
							Key = keyValuePair.Key,
							Value = keyValuePair.Value
						});
			}
			else
			{
				databaseEntries = new MongoDictionaryValue<TKey, TValue>[] { };
			}

			var ser = new ArraySerializer<MongoDictionaryValue<TKey, TValue>>();
			ser.Serialize(bsonWriter, typeof(MongoDictionaryValue<TKey, TValue>[]), databaseEntries.ToArray(), options);
		}

		public bool GetDocumentId(object document, out object id, out Type idNominalType, out IIdGenerator idGenerator)
		{
			idGenerator = null;
			id = null;

			idNominalType = null;
			return false;
		}

		public void SetDocumentId(object document, object id)
		{
			return;
		}
	}
}