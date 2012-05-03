using System;
using System.Collections.Generic;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Guidelines.Ioc.Bootstrap
{
	/// <summary>
	/// Bootstrap foundation class that ensure a bootstrap process occurs only once.
	/// </summary>
	public interface IBootstrapper : IDisposable
	{
		/// <summary>
		/// Call to begin bootstrapping. Can only be run once.
		/// </summary>
		IBootstrapper Bootstrap();

		IContainer Container { get; set; }
		IDependencyRegistrar DependencyRegistrar { get; set; }
		IEnumerable<Registry> AdditionalRegistries { get; set; }
	}
}
