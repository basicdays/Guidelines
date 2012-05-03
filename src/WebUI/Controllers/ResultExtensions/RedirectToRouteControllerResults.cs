using System.Web.Mvc;
using System.Web.Routing;

namespace Guidelines.WebUI.Controllers.ResultExtensions
{
	public static class RedirectToRouteControllerResults
	{

		public static RedirectToRouteResult RedirectToRoute(this Controller controller, object routeValues)
		{
			return RedirectToRoute(controller, new RouteValueDictionary(routeValues));
		}

		public static RedirectToRouteResult RedirectToRoute(this Controller controller, RouteValueDictionary routeValues)
		{
			return RedirectToRoute(null /* routeName */, routeValues);
		}

		public static RedirectToRouteResult RedirectToRoute(this Controller controller, string routeName)
		{
			return RedirectToRoute(controller, routeName, (RouteValueDictionary)null);
		}

		public static RedirectToRouteResult RedirectToRoute(this Controller controller, string routeName, object routeValues)
		{
			return RedirectToRoute(controller, routeName, new RouteValueDictionary(routeValues));
		}

		public static RedirectToRouteResult RedirectToRoute(this Controller controller, string routeName, RouteValueDictionary routeValues)
		{
			return new RedirectToRouteResult(routeName, RouteValuesHelpers.GetRouteValues(routeValues));
		}

	}
}