using System.Web;
using System.Web.Mvc;

namespace BarNapkin.Infrastructure.WebUI.Filters
{
    public class TransferResult : RedirectResult
    {
        public TransferResult(string url)
            : base(url)
        { }

        public TransferResult(object routeValues)
            : base(GetRouteURL(routeValues))
        { }

        private static string GetRouteURL(object routeValues)
        {
            return UrlFinder.Url.RouteUrl(routeValues);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var httpContext = HttpContext.Current;

            //Transfering breaks temp data so just destroy it.  You wont need it on the error page anyway.
            context.Controller.TempData.Clear();

            // MVC 3 running on IIS 7+
            if (HttpRuntime.UsingIntegratedPipeline)
            {
                httpContext.Server.TransferRequest(Url, false); 
            }
            else
            {
                // Pre MVC 3
                var thisJttpContext = context.HttpContext;
                thisJttpContext.RewritePath(Url, false);
                IHttpHandler httpHandler = new MvcHttpHandler();
                httpHandler.ProcessRequest(HttpContext.Current);
            }
        }

    }
}