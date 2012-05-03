using System.Web;

namespace Guidelines.WebUI
{
    public class UrlFinder
    {
        private const string UrlGeneratorKey = "url-generator";

        public static System.Web.Mvc.UrlHelper Url
        {
            get
            {
                var currentContext = HttpContext.Current;

                System.Web.Mvc.UrlHelper urlGenerator;
                    
                if(currentContext.Items[UrlGeneratorKey] != null)
                {
                    urlGenerator = (System.Web.Mvc.UrlHelper) currentContext.Items[UrlGeneratorKey];
                } 
                else
                {
                    urlGenerator = new System.Web.Mvc.UrlHelper(currentContext.Request.RequestContext);
                    currentContext.Items[UrlGeneratorKey] = urlGenerator;
                }

                return urlGenerator;
            }
        }
    }
}