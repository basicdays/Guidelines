using Guidelines.Core;
using MongoDB.Bson;

namespace Guidelines.DataAccess.Mongo.Helpers
{
    public class TextSerializer : ITextSerializer
    {
        public string Serialize<T>(T obj)
        {
            return obj.ToJson();
        }
    }
}
