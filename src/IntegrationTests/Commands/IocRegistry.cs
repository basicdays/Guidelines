using System.ComponentModel.Design;
using Guidelines.Core;
using Guidelines.Core.Bootstrap;
using Guidelines.Ioc.StructureMap;
using Guidelines.Ioc.StructureMap.Adaptors;
using Guidelines.Mapping.AutoMapper;
using Microsoft.Practices.ServiceLocation;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Guidelines.IntegrationTests.Commands
{
	public class IocRegistry : Registry
	{
		public IocRegistry(IContainer container)
		{
			For<IServiceContainer>().Singleton().Use<StructureMapServiceContainer>();

			For<IServiceLocator>().Use(context => {
				try {
					return ServiceLocator.Current;
				} catch {
					return new StructureMapAdaptor(container);
				}
			});
			For<IBootstrapTask>().Add<AutoMapperTask>();
			For<IApplicationServiceProvider>().Use<ApplicationServiceProvider>();
		}
	}
}
