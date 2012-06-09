using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace Guidelines.Ioc.StructureMap.Adaptors
{
	/// <summary>
	/// Adapter used for <see cref="ServiceLocator"/> for StructureMap. Set by using <see cref="ServiceLocator.SetLocatorProvider"/>.
	/// </summary>
	public class StructureMapAdaptor : ServiceLocatorImplBase
	{
		/// <summary>
		/// Creates a new adaptor with the supplied container.
		/// </summary>
		/// <param name="container">A container to use for the service locator.</param>
		public StructureMapAdaptor(IContainer container)
		{
			_container = container;
		}

		private readonly IContainer _container;

		/// <summary>
		/// Returns an instance of <paramref name="serviceType"/>, and uses <paramref name="key"/> if it is not null. If <paramref name="serviceType"/> is a class,
		/// it will construct that class and perform dependency injection regardless if it is registered in the container.
		/// </summary>
		/// <param name="serviceType">The type to construct.</param>
		/// <param name="key">A key to use in the container. Will not be used if null or empty.</param>
		/// <returns>The constructed type from Structuremap.</returns>
		/// <exception cref="StructureMapException">If there was an error constructing the type, or if there was nothing registered for an interface.</exception>
		protected override object DoGetInstance(Type serviceType, string key)
		{
			return string.IsNullOrEmpty(key)
				? _container.GetInstance(serviceType)
				: _container.GetInstance(serviceType, key);
		}

		/// <summary>
		/// Returns all instances of <paramref name="serviceType"/>. If none are found, an empty enumeration is returned.
		/// </summary>
		/// <param name="serviceType">The type to check for anything registered on this type.</param>
		/// <returns>An enumeration of all types found on <paramref name="serviceType"/>.</returns>
		/// <exception cref="StructureMapException">If there was an error constructing the enumeration.</exception>
		protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
		{
			return _container.GetAllInstances(serviceType).Cast<object>();
		}
	}
}
