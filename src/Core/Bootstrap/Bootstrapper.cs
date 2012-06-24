using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
		private readonly ILogger _logger;
		private readonly IDependencyRegistrar _dependencyRegistrar;

		protected static readonly object Padlock = new object();

		/// <summary>
		/// Marks whether or not bootstrapping has taken place.
		/// </summary>
		protected static bool Bootstrapped;

		private IList<IDisposable> DisposableTasks { get; set; }

		public Bootstrapper(IDependencyRegistrar dependencyRegistrar, ILogger logger)
		{
			DisposableTasks = new List<IDisposable>();
			_logger = logger;
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

				LogPortal.SetLogger(_logger);

				var stopwatch = new Stopwatch();
				_logger.Info("Bootstrapping");

				_logger.Debug("Running DependencyTask");
				stopwatch.Start();
				_dependencyRegistrar.ConfigureDependencies();
				stopwatch.Stop();
				_logger.DebugFormat("Completed DependencyTask in {0}ms", stopwatch.ElapsedMilliseconds);
				stopwatch.Reset();

				var startupTasks = _dependencyRegistrar.ResolveStartupTasks().OrderBy(task => task.Order).ToList();
				foreach (var bootstrapTask in startupTasks) {
					var type = bootstrapTask.GetType();

					if (type.IsDefined(typeof(SkipTaskAttribute), false)) {
						continue;
					}

					_logger.Debug("Running " + type.Name);
					stopwatch.Start();

					try
					{
						bootstrapTask.Bootstrap();
					}
					catch (Exception e)
					{
						_logger.Error("Bootstrapping Failed", e);
						throw;
					}

					stopwatch.Stop();
					_logger.DebugFormat("Completed {0} in {1}ms", type.Name, stopwatch.ElapsedMilliseconds);
					stopwatch.Reset();

					if (typeof(IDisposable).IsAssignableFrom(type)) {
						DisposableTasks.Add((IDisposable) bootstrapTask);
					}
				}

				_logger.Debug("Bootstrapping Completed");
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
