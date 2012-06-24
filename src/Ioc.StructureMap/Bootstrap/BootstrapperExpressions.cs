using System.Collections.Generic;
using Guidelines.Core;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Guidelines.Ioc.StructureMap.Bootstrap
{
	public static class BootstrapperExpressions
	{
		public static Bootstrap WithContainer(this Bootstrap bootstrapper, IContainer container)
		{
			bootstrapper.Container = container;
			return bootstrapper;
		}

		public static Bootstrap WithRegistrar(this Bootstrap bootstrapper, IStructuremapRegistrar registrar)
		{
			bootstrapper.StructuremapRegistrar = registrar;
			return bootstrapper;
		}

		public static Bootstrap WithLogger(this Bootstrap bootstrapper, ILogger logger)
		{
			bootstrapper.Logger = logger;
			return bootstrapper;
		}

		public static Bootstrap AdditionalRegristriesAre(this Bootstrap bootstrapper, IEnumerable<Registry> additionalRegistries)
		{
			bootstrapper.AdditionalRegistries = additionalRegistries;
			return bootstrapper;
		}
	}
}