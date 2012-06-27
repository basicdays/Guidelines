using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace Guidelines.WebUI.ActionInvokers
{
	public interface ICommandActionInvoker 
	{
		ActionResult Invoke(IActionMethodResult actionReturnValue, ControllerContext controllerContext);
	}

	public class CommandActionInvoker : ICommandActionInvoker
	{
		private readonly IServiceLocator _container;

		public CommandActionInvoker(IServiceLocator container)
		{
			_container = container;
		}

		public ActionResult Invoke(IActionMethodResult actionReturnValue, ControllerContext controllerContext)
		{
			var openWrappedType = typeof(ActionMethodResultInvokerFacade<>);
			var actionMethodResultType = actionReturnValue.GetType();
			var wrappedResultType = openWrappedType.MakeGenericType(actionMethodResultType);

			var invokerFacade = (IActionMethodResultInvoker)_container.GetInstance(wrappedResultType);

			return invokerFacade.Invoke(actionReturnValue, controllerContext);
		}
	}
}