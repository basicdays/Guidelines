using Guidelines.Ioc.StructureMap.Bootstrap;
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

			//act
			var bootstrapper = new Bootstrap()
				.WithContainer(container)
				.WithRegistrar(registrar)
				.Start();

			bootstrapper.Dispose();

			//assert
			container.AssertConfigurationIsValid();
		}
	}
}
