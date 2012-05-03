using System.Web.Mvc;
using System.Web.Routing;

namespace Guidelines.WebUI.Controllers.ResultExtensions
{
	public static class RedirectToActionControllerResults
	{
		public static RedirectToRouteResult RedirectToAction(this Controller controller, string actionName)
		{
			return RedirectToAction(controller, actionName, (RouteValueDictionary)null);
		}

		public static RedirectToRouteResult RedirectToAction(this Controller controller, string actionName, object routeValues)
		{
			return RedirectToAction(controller, actionName, new RouteValueDictionary(routeValues));
		}

		public static RedirectToRouteResult RedirectToAction(this Controller controller, string actionName, RouteValueDictionary routeValues)
		{
			return RedirectToAction(controller, actionName, null /* controllerName */, routeValues);
		}

		public static RedirectToRouteResult RedirectToAction(this Controller controller, string actionName, string controllerName)
		{
			return RedirectToAction(controller, actionName, controllerName, (RouteValueDictionary)null);
		}

		public static RedirectToRouteResult RedirectToAction(this Controller controller, string actionName, string controllerName, object routeValues)
		{
			return RedirectToAction(controller, actionName, controllerName, new RouteValueDictionary(routeValues));
		}

		public static RedirectToRouteResult RedirectToAction(this Controller controller, string actionName, string controllerName, RouteValueDictionary routeValues)
		{
			RouteValueDictionary mergedRouteValues;

			if (controller.RouteData == null)
			{
				mergedRouteValues = RouteValuesHelpers.MergeRouteValues(actionName, controllerName, null, routeValues, true /* includeImplicitMvcValues */);
			}
			else
			{
				mergedRouteValues = RouteValuesHelpers.MergeRouteValues(actionName, controllerName, controller.RouteData.Values, routeValues, true /* includeImplicitMvcValues */);
			}

			return new RedirectToRouteResult(mergedRouteValues);
		}
	}
}