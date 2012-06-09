using System;
using System.Linq;
using Guidelines.Core.Commands;
using NUnit.Framework;

namespace Guidelines.IntegrationTests.Commands.Fixtures
{
	public class BasicGetCommandTests : CrudCommandTest
	{
		protected TestEntity RepositoryEntity { get; set; }
		protected QueryResult<TestEntity> Result { get; set; }

		private const string TestName = "Mark";

		public class GetTestObject : IGetCommand<TestEntity>
		{
			public Guid Id { get; set; }
		}

		public override void SetupTestContext()
		{
			var id = Guid.NewGuid();
			Repository.Insert(new TestEntity { Id = id, Name = TestName });

			var createCommandProcessor = Container.GetInstance<IQueryProcessor<GetTestObject, TestEntity>>();

			var createCommand = new GetTestObject { Id = id };

			Result = createCommandProcessor.Process(createCommand);
			RepositoryEntity = Repository.GetAll().FirstOrDefault();
		}

		[Test]
		public void TheGetCommandIsSuccesful()
		{
			Assert.That(Result.Successful, Is.True, Result.Successful ? string.Empty : Result.Error.Message);
		}

		[Test]
		public void TheResultingEntityHasTheCorrectName()
		{
			Assert.That(Result.Result.Name, Is.EqualTo(TestName));
		}
	}
}