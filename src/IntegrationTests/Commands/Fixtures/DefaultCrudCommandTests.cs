using System;
using System.Linq;
using Guidelines.Core;
using Guidelines.Core.Bootstrap;
using Guidelines.Core.Commands;
using Guidelines.Ioc.StructureMap.Bootstrap;
using NUnit.Framework;
using StructureMap;

namespace Guidelines.IntegrationTests.Commands.Fixtures
{
	[TestFixture]
	public class DefaultCrudCommandTests
	{
		private Bootstrap _bootstrapper;
		private IContainer _container;
		private MemoryRepository<TestEntity> _repository;

		[TestFixtureSetUp]
		public void Setup()
		{
			_container = new Container();
			_bootstrapper = new Bootstrap()
				.WithContainer(_container)
				.WithRegistrar(new StructuremapRegistrar())
				.Start();
			_repository = _container.GetInstance<MemoryRepository<TestEntity>>();
		}

		[SetUp]
		public void TestSetup()
		{
			_repository.Clear();
		}

		[TestFixtureTearDown]
		public void TearDown()
		{
			_repository.Clear();
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
			var createCommandProcessor = _container.GetInstance<IQueryProcessor<CreateTestObject, TestEntity>>();

			var createCommand = new CreateTestObject{ Name = "Ted" };

			var testEntity = createCommandProcessor.Process(createCommand);
			var createdEntity = _repository.GetAll().FirstOrDefault();

			Assert.That(testEntity.Successful, Is.True, testEntity.Successful ? string.Empty : testEntity.Error.Message);
			Assert.That(testEntity.Result.Name, Is.EqualTo("Ted"));
			Assert.That(testEntity.Result, Is.EqualTo(createdEntity));
			Assert.That(TestUnitOfWork.Commited, Is.True);
		}
	}
}