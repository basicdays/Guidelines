using System.Web;
using System.Web.Mvc;

namespace Guidelines.WebUI.Tools
{
    public static class ControllerContextExtensions
    {
        public static HttpContextBase GetHttpContext(this ControllerContext controllerContext)
        {
            return controllerContext.Controller.ControllerContext.HttpContext;
        }
    }
}