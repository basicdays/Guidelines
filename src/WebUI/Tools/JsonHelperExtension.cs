using System.Web.Script.Serialization;

namespace Guidelines.WebUI.Tools
{
    public static class JsonHelperExtension
    {
        private static readonly JavaScriptSerializer _javaScriptSerializer = new JavaScriptSerializer();

        public static string ToJson<T>(this T toConvertToJson)
        {
            return _javaScriptSerializer.Serialize(toConvertToJson);
        }
    }
}