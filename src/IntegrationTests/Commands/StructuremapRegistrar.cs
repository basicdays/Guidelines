using System.Collections.Generic;
using Guidelines.Core.Commands;
using Guidelines.Ioc.StructureMap;
using Guidelines.Ioc.StructureMap.Adaptors;
using Microsoft.Practices.ServiceLocation;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Guidelines.IntegrationTests.Commands
{
	public class StructuremapRegistrar : IStructuremapRegistrar
	{
		public void ConfigureDependencies(IContainer container, bool setServiceLocator = true)
		{
			ConfigureDependencies(container, new List<Registry>(), setServiceLocator);
		}

		public void ConfigureDependencies(IContainer container, IEnumerable<Registry> registries, bool setServiceLocator = true)
		{
			if (setServiceLocator)
			{
				ServiceLocator.SetLocatorProvider(() => new StructureMapAdaptor(container));
			}

			container.Configure(config =>
			{
				config.AddRegistry<AutoMapperRegistry>();
				config.AddRegistry(new IocRegistry(container));
				config.AddRegistry<TestRegistry>();

				foreach (var registry in registries)
				{
					config.AddRegistry(registry);
				}
			});

			DomainEvents.Container = new ApplicationServiceProvider(container);
		}
	}
}
