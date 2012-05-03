using System.Web.Mvc;

namespace Guidelines.WebUI.ActionInvokers
{
    public class ActionMethodResultInvokerFacade<T> : IActionMethodResultInvoker
        where T : IActionMethodResult
    {
        private readonly IActionMethodResultInvoker<T> _invoker;

        public ActionMethodResultInvokerFacade(IActionMethodResultInvoker<T> invoker)
        {
            _invoker = invoker;
        }

        public ActionResult Invoke(object actionMethodResult, ControllerContext context)
        {
            return _invoker.Invoke((T)actionMethodResult, context);
        }
    }
}