using System.Web.Mvc;

namespace Guidelines.WebUI.Session
{
    public interface IContextRegistrar
    {
        void SetContext(ActionExecutingContext context);
    }
}