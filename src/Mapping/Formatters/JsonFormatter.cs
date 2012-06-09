using System.Web.Script.Serialization;
using AutoMapper;

namespace Guidelines.Mapping.AutoMapper.Formatters
{
    public class JsonFormatter : ValueFormatter<object>
    {
        private readonly JavaScriptSerializer _serializer;

        public JsonFormatter(JavaScriptSerializer javaScriptSerializer)
        {
            _serializer = javaScriptSerializer;
        }

        protected override string FormatValueCore(object value)
        {
            return _serializer.Serialize(value);
        }
    }
}
