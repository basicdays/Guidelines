using System.Text;
using System.Web.Mvc;

namespace Guidelines.WebUI.Controllers.ResultExtensions
{
	public static class JsonControllerResults
	{
		public static JsonResult Json(this Controller controller, object data)
		{
			return Json(controller, data, null /* contentType */);
		}

		public static JsonResult Json(this Controller controller, object data, string contentType)
		{
			return Json(controller, data, contentType, null /* contentEncoding */);
		}

		public static JsonResult Json(this Controller controller, object data, string contentType, Encoding contentEncoding)
		{
			return new JsonResult
			{
				Data = data,
				ContentType = contentType,
				ContentEncoding = contentEncoding
			};
		}

	}
}