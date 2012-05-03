using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Guidelines.WebUI.Controllers;

namespace Guidelines.WebUI.Tools
{
	public class HttpErrorHelper
	{
		public static void HandleError(HttpContext context, Exception exception)
		{
			var httpException = exception as HttpException;

			context.Response.Clear();

			var routeData = new RouteData();
			routeData.Values["controller"] = "Error";
			routeData.Values["action"] = "Unknown";
			routeData.Values["exception"] = exception;

			context.Response.StatusCode = 500;

			if (httpException != null)
			{
				context.Response.StatusCode = httpException.GetHttpCode();

				switch (context.Response.StatusCode)
				{
					case 403:
						routeData.Values["action"] = "Forbidden";
						break;
					case 404:
						routeData.Values["action"] = "NotFound";
						break;
				}
			}

			IController errorsController = new ErrorController();

			var rc = new RequestContext(new HttpContextWrapper(context), routeData);

			try {
				errorsController.Execute(rc);
			} catch(Exception e) {
				context.Response.Write(string.Format("Error rendering error page: {0}<br><br>{1}", e.Message, e.StackTrace));
			}
		}
	}
}
