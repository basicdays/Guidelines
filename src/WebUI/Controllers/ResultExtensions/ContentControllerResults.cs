using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Web.Mvc;

namespace Guidelines.WebUI.Controllers.ResultExtensions
{
	public static class ContentControllerResults
	{
		[SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "0#",
			Justification = "'Content' refers to ContentResult type; 'content' refers to ContentResult.Content property.")]
		public static ContentResult Content(this Controller controller, string content)
		{
			return Content(controller, content, null /* contentType */);
		}

		[SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "0#",
			Justification = "'Content' refers to ContentResult type; 'content' refers to ContentResult.Content property.")]
		public static ContentResult Content(this Controller controller, string content, string contentType)
		{
			return Content(controller, content, contentType, null /* contentEncoding */);
		}

		[SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "0#",
			Justification = "'Content' refers to ContentResult type; 'content' refers to ContentResult.Content property.")]
		public static ContentResult Content(this Controller controller, string content, string contentType, Encoding contentEncoding)
		{
			return new ContentResult
			{
				Content = content,
				ContentType = contentType,
				ContentEncoding = contentEncoding
			};
		}
	}
}