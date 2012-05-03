using System.Web.Mvc;

namespace Guidelines.WebUI.Controllers.ResultExtensions
{
	public static class PartialViewControllerResults
	{
		public static PartialViewResult PartialView(this Controller controller)
		{
			return PartialView(null /* viewName */, null /* model */);
		}

		public static PartialViewResult PartialView(this Controller controller, object model)
		{
			return PartialView(null /* viewName */, model);
		}

		public static PartialViewResult PartialView(this Controller controller, string viewName)
		{
			return PartialView(controller, viewName, null /* model */);
		}

		public static PartialViewResult PartialView(this Controller controller, string viewName, object model)
		{
			if (model != null)
			{
				controller.ViewData.Model = model;
			}

			return new PartialViewResult
			{
				ViewName = viewName,
				ViewData = controller.ViewData,
				TempData = controller.TempData
			};
		}
	}
}