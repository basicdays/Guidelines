using System.Web.Mvc;

namespace Guidelines.WebUI.ActionInvokers
{
	public class IocActionInvoker : ControllerActionInvoker
	{
		private readonly ICommandActionInvoker _actionInvoker;

		public IocActionInvoker(ICommandActionInvoker actionInvoker)
		{
			_actionInvoker = actionInvoker;
		}

		protected override ActionResult CreateActionResult(
			ControllerContext controllerContext,
			ActionDescriptor actionDescriptor,
			object actionReturnValue)
		{
			if (actionReturnValue is IActionMethodResult)
			{
				actionReturnValue = _actionInvoker.Invoke(actionReturnValue as IActionMethodResult, controllerContext);
			}
			return base.CreateActionResult(controllerContext, actionDescriptor, actionReturnValue);
		}
	}
}
