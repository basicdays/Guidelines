using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Guidelines.WebUI.ActionResults
{
    public class LargeJsonResult : ContentResult
    {
        public LargeJsonResult(object toJson)
        {
            var serializer = new JavaScriptSerializer {MaxJsonLength = Int32.MaxValue};

            Content = serializer.Serialize(toJson);

            ContentType = "application/json";
        }
    }
}