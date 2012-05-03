using System.Web.Mvc;

namespace Guidelines.WebUI.Controllers.ResultExtensions
{
	public static class JavascriptControllerResults
	{
		public static JavaScriptResult JavaScript(this Controller controller, string script)
		{
			return new JavaScriptResult { Script = script };
		}

	}
}