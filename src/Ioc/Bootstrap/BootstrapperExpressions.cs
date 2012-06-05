using System.Collections.Generic;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Guidelines.Ioc.Bootstrap
{
	public static class BootstrapperExpressions
	{
		//Review: See bootstrapper constructor review comment
		//Bad - non-guarantied required dependency
		public static IBootstrapper ContainerIs(this IBootstrapper bootstrapper, IContainer container)
		{
			bootstrapper.Container = container;
			return bootstrapper;
		}

		//Bad - non-guarantied required dependency
		public static IBootstrapper RegistrarIs(this IBootstrapper bootstrapper, IDependencyRegistrar registrar)
		{
			bootstrapper.DependencyRegistrar = registrar;
			return bootstrapper;
		}

		//Good - allowing addition of features
		public static IBootstrapper AdditionalRegristriesAre(this IBootstrapper bootstrapper, IEnumerable<Registry> additionalRegistries)
		{
			bootstrapper.AdditionalRegistries = additionalRegistries;
			return bootstrapper;
		}
	}
}