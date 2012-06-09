using System;

namespace Guidelines.Core.Bootstrap
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
	}
}
