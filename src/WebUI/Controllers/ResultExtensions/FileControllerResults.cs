using System.IO;
using System.Web.Mvc;

namespace Guidelines.WebUI.Controllers.ResultExtensions
{
	public static class FileControllerResults
	{
		public static FileContentResult File(this Controller controller, byte[] fileContents, string contentType)
		{
			return File(controller, fileContents, contentType, null /* fileDownloadName */);
		}

		public static FileContentResult File(this Controller controller, byte[] fileContents, string contentType, string fileDownloadName)
		{
			return new FileContentResult(fileContents, contentType) { FileDownloadName = fileDownloadName };
		}

		public static FileStreamResult File(this Controller controller, Stream fileStream, string contentType)
		{
			return File(controller, fileStream, contentType, null /* fileDownloadName */);
		}

		public static FileStreamResult File(this Controller controller, Stream fileStream, string contentType, string fileDownloadName)
		{
			return new FileStreamResult(fileStream, contentType) { FileDownloadName = fileDownloadName };
		}

		public static FilePathResult File(this Controller controller, string fileName, string contentType)
		{
			return File(controller, fileName, contentType, null /* fileDownloadName */);
		}

		public static FilePathResult File(this Controller controller, string fileName, string contentType, string fileDownloadName)
		{
			return new FilePathResult(fileName, contentType) { FileDownloadName = fileDownloadName };
		}
	}
}