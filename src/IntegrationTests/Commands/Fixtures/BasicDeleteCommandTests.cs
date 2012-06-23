using System;
using System.Linq;
using Guidelines.Core.Commands;
using NUnit.Framework;

namespace Guidelines.IntegrationTests.Commands.Fixtures
{
	public class BasicDeleteCommandTests : CrudCommandTest
	{
		protected TestEntity RepositoryEntity { get; set; }
		protected CommandResult Result { get; set; }

		public class Delete : IDeleteCommand<TestEntity>
		{
			public Guid? Id { get; set; }
		}

		public override void SetupTestContext()
		{
			var id = Guid.NewGuid();
			Repository.Insert(new TestEntity { Id = id });

			var createCommandProcessor = Container.GetInstance<ICommandProcessor<Delete>>();

			var createCommand = new Delete { Id = id };

			Result = createCommandProcessor.Process(createCommand);
			RepositoryEntity = Repository.GetAll().FirstOrDefault();
		}

		[Test]
		public void TheDeleteCommandIsSuccesful()
		{
			Assert.That(Result.Successful, Is.True, Result.Successful ? string.Empty : Result.Error.Message);
		}

		[Test]
		public void TheResultingEntityIsNoLongerInTheRepository()
		{
			Assert.That(RepositoryEntity, Is.Null);
		}

		[Test]
		public void TheUnitOfWorkWasCommited()
		{
			Assert.That(TestUnitOfWork.Commited, Is.True);
		}
	}
}