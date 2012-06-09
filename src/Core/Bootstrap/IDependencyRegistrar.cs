using System.Collections.Generic;

namespace Guidelines.Core.Bootstrap
{
	public interface IDependencyRegistrar
	{
		void ConfigureDependencies();

		IEnumerable<IBootstrapTask> ResolveStartupTasks();
	}
}
