using System.Collections;
using System.Web.Mvc;

namespace Guidelines.WebUI.ActionResults
{
	/// <summary>
	/// Naively dynamically changes output based on content-type request header.
	/// Only necessary until ASP.net MVC 4, as this will be obsolete by that point.
	/// </summary>
	/// <remarks>
	/// Protects against JSON hijacking, see http://haacked.com/archive/2009/06/25/json-hijacking.aspx
	/// for more info.
	/// </remarks>
	public class DynamicView : ActionResult
	{
		public DynamicView(object model)
		{
			Model = model;
		}

		public object Model { get; set; }

		public override void ExecuteResult(ControllerContext context)
		{
			var contentType = context.RequestContext.HttpContext.Request.Headers["Accept"];
			if (contentType != null && contentType.Contains("application/json"))
			{
				if (Model is IEnumerable && !(Model is string))
				{
					Model = new CollectionWrapper { Collection = (IEnumerable)Model };
				}
				var result = new JsonResult { Data = Model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
				result.ExecuteResult(context);
			}
			else
			{
				context.Controller.ViewData.Model = Model;
				var result = new ViewResult { ViewData = context.Controller.ViewData };
				result.ExecuteResult(context);
			}
		}
	}
}