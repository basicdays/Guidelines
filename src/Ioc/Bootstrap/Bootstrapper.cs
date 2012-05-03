using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using StructureMap;
using StructureMap.Configuration.DSL;
using log4net;

namespace Guidelines.Ioc.Bootstrap
{
	/// <summary>
	/// Bootstrap foundation class that ensure a bootstrap process occurs only once.
	/// </summary>
	/// <remarks>
	/// Bootstrapping should only occur once. Bootstrapping will use a double check locking singleton pattern with
	/// <see cref="Bootstrapped"/> to see if a bootstrap process has occured.
	/// 
	/// Areas that are constantly edited are extracted out into their own registrar classes to reduce the need to
	/// edit this class.
	/// </remarks>
	public class Bootstrapper : IBootstrapper
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		protected static readonly object Padlock = new object();

		/// <summary>
		/// Marks whether or not bootstrapping has taken place.
		/// </summary>
		protected static bool Bootstrapped;

		public IContainer Container { get; set; }
		public IDependencyRegistrar DependencyRegistrar { get; set; }
		public IEnumerable<Registry> AdditionalRegistries { get; set; }
		private IList<IDisposable> DisposableTasks { get; set; }

		public Bootstrapper()
		{
			AdditionalRegistries = new List<Registry>();
			DisposableTasks = new List<IDisposable>();
		}

		/// <summary>
		/// Call to begin bootstrapping. Can only be run once.
		/// </summary>
		public IBootstrapper Bootstrap()
		{
			if (Bootstrapped) {
				return this;
			}

			lock (Padlock)
			{
				if (Bootstrapped) {
					return this;
				}

				var stopwatch = new Stopwatch();
				Log.Info("Bootstrapping");

				Log.Debug("Running DependencyTask");
				stopwatch.Start();
				DependencyRegistrar.ConfigureDependencies(Container, AdditionalRegistries);
				stopwatch.Stop();
				Log.DebugFormat("Completed DependencyTask in {0}ms", stopwatch.ElapsedMilliseconds);
				stopwatch.Reset();


				foreach (var bootstrapTask in Container.GetAllInstances<IBootstrapTask>()) {
					var type = bootstrapTask.GetType();

					if (type.IsDefined(typeof(SkipTaskAttribute), false)) {
						continue;
					}

					Log.Debug("Running " + type.Name);
					stopwatch.Start();
					bootstrapTask.Bootstrap();
					stopwatch.Stop();
					Log.DebugFormat("Completed {0} in {1}ms", type.Name, stopwatch.ElapsedMilliseconds);
					stopwatch.Reset();

					if (typeof(IDisposable).IsAssignableFrom(type)) {
						DisposableTasks.Add((IDisposable) bootstrapTask);
					}
				}

				Log.Debug("Bootstrapping Completed");
				Bootstrapped = true;
			}
			return this;
		}

		public void Dispose()
		{
			foreach(var disposableTask in DisposableTasks) {
				disposableTask.Dispose();
			}
		}
	}
}
