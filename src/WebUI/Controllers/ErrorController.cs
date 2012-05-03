using System;
using System.Web.Mvc;

namespace Guidelines.WebUI.Controllers
{
    public class ErrorController : Controller
    {
    	private const string unknow = "Unknown";

		public ActionResult Unknown(Exception exception)
		{
			ViewBag.Title = "Unknow Error";

			return View("Error", new HandleErrorInfo(exception, unknow, unknow));
		}

		public ActionResult NotFound(Exception exception)
		{
			ViewBag.Title = "Not Found";

			return View("Error", new HandleErrorInfo(exception, unknow, unknow));
		}

		public ActionResult Forbidden(Exception exception)
		{
			ViewBag.Title = "Forbidden";

			return View("Error", new HandleErrorInfo(exception, unknow, unknow));
		}
    }
}
