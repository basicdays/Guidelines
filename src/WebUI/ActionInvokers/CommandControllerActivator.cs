using System;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.ServiceLocation;

namespace Guidelines.WebUI.ActionInvokers
{
	public class CommandControllerActivator : IControllerActivator
	{
		private readonly IServiceLocator _serviceLocator;
		private readonly IActionInvoker _actionInvoker;

		public CommandControllerActivator(IServiceLocator serviceLocator, IocActionInvoker actionInvoker)
		{
			_serviceLocator = serviceLocator;
			_actionInvoker = actionInvoker;
		}

		public IController Create(RequestContext requestContext, Type controllerType)
		{
			try
			{
				var controller = (IController)_serviceLocator.GetInstance(controllerType);
				if (typeof(Controller).IsAssignableFrom(controller.GetType()))
				{
					((Controller)controller).ActionInvoker = _actionInvoker;
				}
				return controller;
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(
					String.Format(
						CultureInfo.CurrentCulture,
						"An error occurred when trying to create a controller of type '{0}'.",
						controllerType),
					ex);
			}
		}
	}
}