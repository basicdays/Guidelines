using System.Collections.Generic;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Guidelines.Ioc
{
	public interface IDependencyRegistrar
	{
		void ConfigureDependencies(IContainer container, bool setServiceLocator = true);

		void ConfigureDependencies(IContainer container, IEnumerable<Registry> registries, bool setServiceLocator = true);
	}
}
