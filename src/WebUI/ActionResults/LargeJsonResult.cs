using System;
using System.Collections;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Guidelines.WebUI.ActionResults
{
	/// <summary>
	/// For returning json that is to large for the typical handler.  Use this at your own risk.
	/// </summary>
    public class LargeJsonResult : ContentResult
    {
        public LargeJsonResult(object toJson)
        {
            var serializer = new JavaScriptSerializer {MaxJsonLength = Int32.MaxValue};

        	bool needsWrapping = toJson is IEnumerable && !(toJson is string);

			//protect against get based json attack
        	object toSerialize = needsWrapping
        		? new CollectionWrapper {Collection = (IEnumerable) toJson}
        		: toJson;

			Content = serializer.Serialize(toSerialize);

            ContentType = "application/json";
        }
    }
}