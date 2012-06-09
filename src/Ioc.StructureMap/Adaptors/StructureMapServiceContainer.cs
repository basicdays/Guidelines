using System;
using System.ComponentModel.Design;
using StructureMap;

namespace Guidelines.Ioc.StructureMap.Adaptors
{
    /// <summary>
    /// Container that will use <see cref="MSServiceLocator"/> to get types. Does not implement changing the registry.
    /// </summary>
    public class StructureMapServiceContainer : IServiceContainer
    {
        public StructureMapServiceContainer(IContainer container)
        {
            _container = container;
        }

        private readonly IContainer _container;

        /// <summary>
        /// Gets <paramref name="serviceType"/> from <see cref="MSServiceLocator"/>.
        /// </summary>
        /// <param name="serviceType">The type to retrieve from the standard service locator.</param>
        /// <returns>The evaluated type from the container.</returns>
        public object GetService(Type serviceType)
        {
            var instance = _container.GetInstance(serviceType);

            if (instance is ServiceCreatorCallback)
            {
                instance = ((ServiceCreatorCallback)instance)(this, serviceType);
            }

            return instance;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public void AddService(Type serviceType, object serviceInstance)
        {
            AddService(serviceType, serviceInstance, true);
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public void AddService(Type serviceType, object serviceInstance, bool promote)
        {
            _container.Configure(config => config.For(serviceType).Add(serviceInstance));
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        public void AddService(Type serviceType, ServiceCreatorCallback callback)
        {
            AddService(serviceType, callback, true);
        }

        /// <summary>
        /// Adds a delegate to a service type.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="callback"></param>
        /// <param name="promote"></param>
        public void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
        {
            _container.Configure(config => config.For(serviceType).Add(callback));
        }

        /// <summary>
        /// Removes the service from the container, but Structuremap does not support this.
        /// </summary>
        /// <param name="serviceType">Service type to remove from the container.</param>
        /// <exception cref="InvalidOperationException">Every time this is called.</exception>
        public void RemoveService(Type serviceType)
        {
            RemoveService(serviceType, true);
        }

        /// <summary>
        /// Removes the service from the container, but Structuremap does not support this.
        /// </summary>
        /// <param name="serviceType">Service type to remove from the container.</param>
        /// <param name="promote">Not used</param>
        /// <exception cref="InvalidOperationException">Every time this is called.</exception>
        public void RemoveService(Type serviceType, bool promote)
        {
            throw new InvalidOperationException("Structuremap does not support removing services.");
        }
    }
}
