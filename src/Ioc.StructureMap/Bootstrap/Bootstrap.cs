using System;
using System.Collections.Generic;
using Guidelines.Core;
using Guidelines.Core.Bootstrap;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Guidelines.Ioc.StructureMap.Bootstrap
{
	public class Bootstrap : IDisposable
	{
		public IContainer Container { get; set; }
		public ILogger Logger { get; set; }
		public IStructuremapRegistrar StructuremapRegistrar { get; set; }
		public IEnumerable<Registry> AdditionalRegistries { get; set; }

		private Bootstrapper _bootstraper;

		public Bootstrap Start()
		{
			if(Container == null) {
				throw new BootstrapException("A container is required!");
			}
			if (StructuremapRegistrar == null){
				throw new BootstrapException("the structure map configuration registrar is required!");
			}
			if (Logger == null) {
				throw new BootstrapException("Some kind of logger is required!");
			}

			var dependencyRegistrar = new DependencyRegistrar(Container, StructuremapRegistrar, AdditionalRegistries);
			_bootstraper = new Bootstrapper(dependencyRegistrar, Logger);

			_bootstraper.Bootstrap();
			return this;
		}

		public void Dispose()
		{
			_bootstraper.Dispose();
		}
	}
}
