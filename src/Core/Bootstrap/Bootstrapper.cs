using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using log4net;

namespace Guidelines.Core.Bootstrap
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
		private readonly IDependencyRegistrar _dependencyRegistrar;

		protected static readonly object Padlock = new object();

		/// <summary>
		/// Marks whether or not bootstrapping has taken place.
		/// </summary>
		protected static bool Bootstrapped;

		private IList<IDisposable> DisposableTasks { get; set; }

		//Review: Because we have required dependencies being loaded via extension methods, we are allowing the creating of invalid bostrappers
		//guarantied to fail.  While I know the extensions look pretty, they are dangerous.  They really should only be for optional configuration
		//items that you can use to add in or modify things.  They should not be used to supply required components for function.
		public Bootstrapper(IDependencyRegistrar dependencyRegistrar)
		{
			DisposableTasks = new List<IDisposable>();
			_dependencyRegistrar = dependencyRegistrar;
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
				_dependencyRegistrar.ConfigureDependencies();
				stopwatch.Stop();
				Log.DebugFormat("Completed DependencyTask in {0}ms", stopwatch.ElapsedMilliseconds);
				stopwatch.Reset();

				var startupTasks = _dependencyRegistrar.ResolveStartupTasks().OrderBy(task => task.Order).ToList();
				foreach (var bootstrapTask in startupTasks) {
					var type = bootstrapTask.GetType();

					if (type.IsDefined(typeof(SkipTaskAttribute), false)) {
						continue;
					}

					Log.Debug("Running " + type.Name);
					stopwatch.Start();

					try
					{
						bootstrapTask.Bootstrap();
					}
					catch (Exception e)
					{
						Log.Error("Bootstrapping Failed", e);
						throw;
					}

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
			//Review: I have to do this to get tests working.
			Bootstrapped = false;
		}
	}
}
