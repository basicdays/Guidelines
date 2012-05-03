using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace Guidelines.WebUI.ActionInvokers
{
	public class IocActionInvoker : ControllerActionInvoker
	{
		private readonly IServiceLocator _container;

		public IocActionInvoker(IServiceLocator container)
		{
			_container = container;
		}

		protected override ActionResult CreateActionResult(
			ControllerContext controllerContext,
			ActionDescriptor actionDescriptor,
			object actionReturnValue)
		{
			if (actionReturnValue is IActionMethodResult)
			{
				var openWrappedType = typeof(ActionMethodResultInvokerFacade<>);
				var actionMethodResultType = actionReturnValue.GetType();
				var wrappedResultType = openWrappedType.MakeGenericType(actionMethodResultType);

				var invokerFacade = (IActionMethodResultInvoker)_container.GetInstance(wrappedResultType);

				actionReturnValue = invokerFacade.Invoke(actionReturnValue, controllerContext);
			}
			return base.CreateActionResult(controllerContext, actionDescriptor, actionReturnValue);
		}
	}
}
