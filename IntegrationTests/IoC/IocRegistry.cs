using System.ComponentModel.Design;
using Guidelines.AutoMapper;
using Guidelines.Domain;
using Guidelines.Ioc;
using Guidelines.Ioc.Adaptors;
using Guidelines.Ioc.Bootstrap;
using Microsoft.Practices.ServiceLocation;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Guidelines.IntegrationTests.IoC
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
