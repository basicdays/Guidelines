using System.Web.Mvc;
using Guidelines.WebUI.ActionResults;

namespace Guidelines.WebUI.Controllers.ResultExtensions
{
	public static class CusotomControllerResults
	{
		public static LargeJsonResult LargeJson(this Controller controller, object model)
		{
			return new LargeJsonResult(model);
		}

		public static DownloadResult Download(this Controller controller, string filePath, string fileName)
		{
			return new DownloadResult(filePath, fileName);
		}

		public static ImageResult Image(this Controller controller, string filePath, int maxWidth = 500, int maxHeight = 500)
		{
			return new ImageResult(filePath, maxWidth, maxHeight);
		}
	}
}