using System.Web.Mvc;

namespace Guidelines.WebUI.ActionInvokers
{
    public interface IActionResultInvoker<TActionResult>
        where TActionResult : ActionResult
    {
        void ExecuteResult(TActionResult actionResult, ControllerContext context);
    }
}