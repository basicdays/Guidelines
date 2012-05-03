using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace Guidelines.WebUI
{
	public class ServiceLocatorDependencyResolver : IDependencyResolver
	{
		public ServiceLocatorDependencyResolver(IServiceLocator serviceLocator)
		{
			_serviceLocator = serviceLocator;
		}

		private readonly IServiceLocator _serviceLocator;

		public object GetService(Type serviceType)
		{
			try {
				return _serviceLocator.GetInstance(serviceType);
			}
			catch {
				return null;
			}
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return _serviceLocator.GetAllInstances(serviceType);
		}
	}
}
