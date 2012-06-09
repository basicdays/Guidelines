using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Guidelines.Domain.Commands;
using Guidelines.Ioc;
using Guidelines.Ioc.Adaptors;
using Microsoft.Practices.ServiceLocation;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Guidelines.IntegrationTests.IoC
{
	public class DependencyRegistrar : IDependencyRegistrar
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
