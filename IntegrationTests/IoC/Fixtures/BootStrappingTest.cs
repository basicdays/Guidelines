using Guidelines.Ioc.Bootstrap;
using NUnit.Framework;
using StructureMap;

namespace Guidelines.IntegrationTests.IoC.Fixtures
{
	[TestFixture]
	public class BootStrappingTest
	{
		[Test]
		public void CanSetupContainer()
		{
			//arrange
			var container = new Container();
			var registrar = new DependencyRegistrar();

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
			var registrar = new DependencyRegistrar();

			//act
			var bootstrapper = new Bootstrapper()
				.ContainerIs(container)
				.RegistrarIs(registrar)
				.Bootstrap();

			bootstrapper.Dispose();

			//assert
			container.AssertConfigurationIsValid();
		}
	}
}
