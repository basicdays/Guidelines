using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using BarNapkin.Infrastructure.WebUI.Properties;
using BarNapkin.Infrastructure.WebUI.Endpoints;

namespace BarNapkin.Infrastructure.WebUI.Filters.Errors
{
    public class RedirectToActionOnErrorAttribute : RedirectOnErrorAttribute
    {
        private readonly List<Type> _types;

        public RedirectToActionOnErrorAttribute(params Type[] type)
            : this(typeof(ErrorController), "Error", type)
        { }

        public RedirectToActionOnErrorAttribute()
            : this(typeof(ErrorController), "Error", new Type[] { })
        { }

        public RedirectToActionOnErrorAttribute(Type controller, string action, params Type[] type)
        {
            Controller = controller;
            Action = action;
            _types = type.ToList();
        }

        public override sealed List<Type> Types { get { return _types; } }
        public Type Controller { get; private set; }
        public string Action { get; private set; }

        protected override bool Validate(ActionExecutedContext filterContext)
        {
            //the url property is always needed
            if (Controller == null || (string.IsNullOrEmpty(Action) || Action.Trim() == string.Empty))
				throw new ArgumentNullException(Resources.Error_RedirectUrlMustHaveValue, new Exception(Resources.Error_UrlIsEmpty));

            //make sure the Contoller property is a Controller
            if (!typeof(Controller).IsAssignableFrom(Controller))
				throw new ArgumentException(Resources.Error_ControllerNotDerivedFromBase);

            //continue processing
            return true;
        }

        protected override void Redirect(ActionExecutedContext filterContext)
        {
            //Turn "Foo.Foo.Foo.BarController" into "Bar"
            string controllerName = Controller.ToString();
            controllerName = controllerName.Substring(controllerName.LastIndexOf(".") + 1);
            controllerName = controllerName.Substring(0, controllerName.LastIndexOf("Controller"));

            //turn route identity into url
            RouteValueDictionary rvd = new RouteValueDictionary(
                new
                {
                    controller = controllerName,
                    action = Action
                });

            ControllerContext ctx = new ControllerContext(
                filterContext.HttpContext,
                filterContext.RouteData,
                filterContext.Controller
                );

            VirtualPathData vpd = RouteTable.Routes.GetVirtualPath(ctx.RequestContext, rvd);
            string url = vpd.VirtualPath;

            //Redirect
            PlaceInItemDictionary("Error", filterContext.Exception, filterContext);
            PlaceInItemDictionary("Controller", filterContext.ActionDescriptor.ControllerDescriptor.ControllerName, filterContext);
            PlaceInItemDictionary("Action", filterContext.ActionDescriptor.ActionName, filterContext);

            filterContext.ExceptionHandled = true;
            var transferResult = new TransferResult(url);
            transferResult.ExecuteResult(filterContext.Controller.ControllerContext);
            //filterContext.HttpContext.Server.Transfer( url, true );
        }
    }
}