using System;
using System.Web.Mvc;

namespace Guidelines.WebUI.Errors
{
    public interface IErrorAspect
    {
        ErrorContext SetErrorContext(ControllerContext context, Exception ex);
        ErrorContext SetErrorContext(ControllerContext context, ModelStateDictionary modelState);

        ErrorContext BuildErrorContext(ControllerContext context, Exception ex);
        ErrorContext BuildErrorContext(ControllerContext context, ModelStateDictionary modelState);
    }
}