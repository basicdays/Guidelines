using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace Guidelines.WebUI.Controllers.ResultExtensions
{
	public static class RedirectControllerResults
	{

		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
			Justification = "Instance method for consistency with other helpers.")]
		[SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#",
			Justification = "Response.Redirect() takes its URI as a string parameter.")]
		public static RedirectResult Redirect(this Controller controller, string url)
		{
			if (String.IsNullOrEmpty(url))
			{
				throw new ArgumentException("Value cannot be null or empty.", "url");
			}
			return new RedirectResult(url);
		}

	}
}