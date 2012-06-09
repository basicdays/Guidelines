using System;
using Guidelines.Domain;
using Guidelines.Domain.Commands;
using Guidelines.Ioc.Bootstrap;
using NUnit.Framework;
using StructureMap;
using IBootstrapper = Guidelines.Ioc.Bootstrap.IBootstrapper;

namespace Guidelines.IntegrationTests.IoC
{
	[TestFixture]
	public class DefaultCrudCommandTests
	{
		private IBootstrapper _bootstrapper;

		[TestFixtureSetUp]
		public void Setup()
		{
			_bootstrapper = new Bootstrapper()
				.ContainerIs(new Container())
				.RegistrarIs(new DependencyRegistrar())
				.Bootstrap();
		}

		[TestFixtureTearDown]
		public void TearDown()
		{
			_bootstrapper.Dispose();
		}

		public class TestEntity : EntityBase<TestEntity>
		{
			public string Name { get; set; }
		}

		public class CreateTestObject : ICreateCommand<TestEntity>
		{
			public string Name { get; set; }
		}

		public class UpdateTestObject : IUpdateCommand<TestEntity>
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
		}

		[Test]
		public void CanCreateNewEntity()
		{
			var createCommandProcessor = _bootstrapper.Container.GetInstance<IQueryProcessor<CreateTestObject, TestEntity>>();

			var createCpmmand = new CreateTestObject{ Name = "Ted" };

			var testEntity = createCommandProcessor.Process(createCpmmand);

			Assert.That(testEntity.Successful, Is.True);
			Assert.That(testEntity.Result.Name, Is.EqualTo("Ted"));
		}
	}
}