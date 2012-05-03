using System;
using System.Linq;
using System.Web.Mvc;
using BarNapkin.Infrastructure.WebUI.Properties;

namespace BarNapkin.Infrastructure.WebUI.Filters.Errors
{
    public sealed class RedirectToUrlOnErrorAttribute : RedirectOnErrorAttribute
    {
        public RedirectToUrlOnErrorAttribute(string url, params Type[] type)
        {
            Types = type.ToList();
            Url = url;
        }

        public RedirectToUrlOnErrorAttribute(string url)
        {
            Types = null;
            Url = url;
        }

        public string Url{ get; set; }

        protected override bool Validate(ActionExecutedContext filterContext)
        {
            //the url property is always needed
            if( string.IsNullOrEmpty( Url ) || Url.Trim() == string.Empty )
				throw new ArgumentNullException(Resources.Error_RedirectUrlMustHaveValue, new Exception(Resources.Error_UrlIsEmpty));

            //continue execution
            return true;

        }

        protected override void Redirect(ActionExecutedContext filterContext)
        {
            PlaceInItemDictionary("Error", filterContext.Exception, filterContext);
            PlaceInItemDictionary("Controller", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext);
            PlaceInItemDictionary("Action", filterContext.ActionDescriptor.ActionName, filterContext);
            filterContext.ExceptionHandled = true;
            //filterContext.HttpContext.Server.Transfer(Url, true);
            var transferResult = new TransferResult(Url);
            transferResult.ExecuteResult(filterContext.Controller.ControllerContext);
        }

    }
}