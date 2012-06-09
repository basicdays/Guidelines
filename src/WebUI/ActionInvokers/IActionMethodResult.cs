using System.Web.Mvc;
using Guidelines.Core;
using Guidelines.WebUI.Errors;

namespace Guidelines.WebUI.ActionInvokers
{
    public interface IActionMethodResult
    {
        bool NoErrorState { get; set; }
        ActionResult Failure(IGenericMapper mapper, ErrorContext errorContext);
    }

    public interface IActionMethodResultInvoker<T>
        where T : IActionMethodResult
    {
        ActionResult Invoke(T actionMethodResult, ControllerContext context);
    }

    public interface IActionMethodResultInvoker
    {
        ActionResult Invoke(object actionMethodResult, ControllerContext context);
    }
}
