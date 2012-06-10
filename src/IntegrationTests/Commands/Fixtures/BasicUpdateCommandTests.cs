using System;
using System.Linq;
using Guidelines.Core.Commands;
using NUnit.Framework;

namespace Guidelines.IntegrationTests.Commands.Fixtures
{
	public class BasicUpdateCommandTests : CrudCommandTest
	{
		protected TestEntity RepositoryEntity { get; set; }
		protected CommandResult Result { get; set; }

		protected const string InitialTestName = "Mark";
		protected const string UpdatedTestName = "Ted";

		public class Update : IUpdateCommand<TestEntity>
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
		}

		public override void SetupTestContext()
		{
			var id = Guid.NewGuid();
			Repository.Insert(new TestEntity { Id = id, Name = InitialTestName });

			var createCommandProcessor = Container.GetInstance<ICommandProcessor<Update>>();

			var createCommand = new Update { Id = id, Name = UpdatedTestName };

			Result = createCommandProcessor.Process(createCommand);
			RepositoryEntity = Repository.GetAll().FirstOrDefault();
		}

		[Test]
		public void TheUpdateCommandIsSuccesful()
		{
			Assert.That(Result.Successful, Is.True, Result.Successful ? string.Empty : Result.Error.Message);
		}

		[Test]
		public void TheResultingEntityIsTheSameAsWhatIsInTheRepository()
		{
			Assert.That(RepositoryEntity.Name, Is.EqualTo(UpdatedTestName));
		}

		[Test]
		public void TheUnitOfWorkWasCommited()
		{
			Assert.That(TestUnitOfWork.Commited, Is.True);
		}
	}
}