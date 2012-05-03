using System.Collections.Generic;
using System.Web.Mvc;
using BarNapkin.Infrastructure.WebUI.Session;

namespace BarNapkin.Infrastructure.WebUI.Filters.Session
{
    public class AspectRegistrarFilter : ActionFilterAttribute
    {
        public IEnumerable<IContextRegistrar> UiAspects { get; set; }
        public IEnumerable<IContextAction> UiActions { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
	    {
	        base.OnActionExecuting(filterContext);

            foreach (IContextRegistrar uiAspect in UiAspects)
            {
                uiAspect.SetContext(filterContext);
            }
	    }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            foreach (IContextAction uiAction in UiActions)
            {
                uiAction.TakePreRenderAction(filterContext);
            }
        } 
    }
}