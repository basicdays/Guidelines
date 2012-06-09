using System.Collections.Generic;
using Guidelines.Core.Bootstrap;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Guidelines.Ioc.StructureMap.Bootstrap
{
	public class DependencyRegistrar : IDependencyRegistrar
	{
		public IContainer Container { get; set; }
		public IStructuremapRegistrar StructuremapRegistrar { get; set; }
		public IEnumerable<Registry> AdditionalRegistries { get; set; }

		public DependencyRegistrar(IContainer container, IStructuremapRegistrar structuremapRegistrar, IEnumerable<Registry> additionalRegistries = null)
		{
			Container = container;
			StructuremapRegistrar = structuremapRegistrar;
			AdditionalRegistries = additionalRegistries ?? new List<Registry>();
		}

		public void ConfigureDependencies()
		{
			StructuremapRegistrar.ConfigureDependencies(Container, AdditionalRegistries);
		}

		public IEnumerable<IBootstrapTask> ResolveStartupTasks()
		{
			return Container.GetAllInstances<IBootstrapTask>();
		}
	}
}