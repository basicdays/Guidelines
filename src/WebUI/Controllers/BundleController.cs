using System.Web.Mvc;
using SquishIt.Framework;

namespace Guidelines.WebUI.Controllers
{
    /// <summary>
    /// Provides actions to handle javasript and css file requests as a result
    /// of the SquishIt framework default routes.
    /// </summary>
    public class BundleController : Controller
    {
        //
        // GET: /Bundle/Style
        [OutputCache(Duration = int.MaxValue, VaryByParam = "file")]
        public virtual ContentResult Style(string file)
        {
            Response.Cache.SetOmitVaryStar(true);
            var output = Bundle.Css().RenderCached(file);
            return new ContentResult { Content = output, ContentType = "text/css" };
        }

        //
        // GET: /Bundle/Script
        [OutputCache(Duration = int.MaxValue, VaryByParam = "file")]
        public virtual ContentResult Script(string file)
        {
            Response.Cache.SetOmitVaryStar(true);
			var output = Bundle.JavaScript().RenderCached(file);
            return new ContentResult { Content = output, ContentType = "text/javascript" };
        }

    }
}
