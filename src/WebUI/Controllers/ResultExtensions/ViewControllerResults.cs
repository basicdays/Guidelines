using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace Guidelines.WebUI.Controllers.ResultExtensions
{
	public static class ViewControllerResults
	{
		public static ViewResult View(this Controller controller)
		{
			return View(controller, null /* viewName */, null /* masterName */, null /* model */);
		}

		public static ViewResult View(this Controller controller, object model)
		{
			return View(controller, null /* viewName */, null /* masterName */, model);
		}

		public static ViewResult View(this Controller controller, string viewName)
		{
			return View(controller, viewName, null /* masterName */, null /* model */);
		}

		public static ViewResult View(this Controller controller, string viewName, string masterName)
		{
			return View(controller, viewName, masterName, null /* model */);
		}

		public static ViewResult View(this Controller controller, string viewName, object model)
		{
			return View(controller, viewName, null /* masterName */, model);
		}

		public static ViewResult View(this Controller controller, string viewName, string masterName, object model)
		{
			if (model != null)
			{
				controller.ViewData.Model = model;
			}

			return new ViewResult
			{
				ViewName = viewName,
				MasterName = masterName,
				ViewData = controller.ViewData,
				TempData = controller.TempData
			};
		}

		[SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "0#",
			Justification = "The method name 'View' is a convenient shorthand for 'CreateViewResult'.")]
		public static ViewResult View(this Controller controller, IView view)
		{
			return View(controller, view, null /* model */);
		}

		[SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "0#",
			Justification = "The method name 'View' is a convenient shorthand for 'CreateViewResult'.")]
		public static ViewResult View(this Controller controller, IView view, object model)
		{
			if (model != null)
			{
				controller.ViewData.Model = model;
			}

			return new ViewResult
			{
				View = view,
				ViewData = controller.ViewData,
				TempData = controller.TempData
			};
		}
	}
}