using System.Web.Mvc;

namespace Guidelines.WebUI.Session
{
    public interface IContextAction
    {
        void TakePreRenderAction(ActionExecutedContext context);
    }
}