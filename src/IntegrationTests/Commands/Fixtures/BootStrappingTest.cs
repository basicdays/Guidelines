using Guidelines.Ioc.StructureMap.Bootstrap;
using Guidelines.Logging.Log4Net;
using NUnit.Framework;
using StructureMap;

namespace Guidelines.IntegrationTests.Commands.Fixtures
{
	[TestFixture]
	public class BootStrappingTest
	{
		[Test]
		public void CanSetupContainer()
		{
			//arrange
			var container = new Container();
			var registrar = new StructuremapRegistrar();

			//act
			registrar.ConfigureDependencies(container, false);

			//assert
			container.AssertConfigurationIsValid();
		}

		[Test]
		public void CanBootstrapTheIocProject()
		{
			//arrange
			var container = new Container();
			var registrar = new StructuremapRegistrar();
			var logger = new Logger<BootStrappingTest>();

			//act
			var bootstrapper = new Bootstrap()
				.WithContainer(container)
				.WithRegistrar(registrar)
				.WithLogger(logger)
				.Start();

			bootstrapper.Dispose();

			//assert
			container.AssertConfigurationIsValid();
		}
	}
}
