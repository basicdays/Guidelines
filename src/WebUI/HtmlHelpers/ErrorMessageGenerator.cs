using System.Web.Mvc;
using Guidelines.WebUI.Errors;

namespace Guidelines.WebUI.HtmlHelpers
{
    public class ErrorMessageGenerator
    {
        public static string GenerateErrorMessages(ErrorContext errorContext)
        {
            string errorHtml = string.Empty;

            if(errorContext.HasMessages)
            {
                errorHtml = errorContext.ErrorMessage;
            }

            return errorHtml;
        }

        public static string GenerateErrorMessages(TempDataDictionary tempData)
        {
            var errorContext = (ErrorContext)tempData[ErrorAspect.ErrorContextKey];

            return errorContext != null ? GenerateErrorMessages(errorContext) : string.Empty;
        }
    }
}