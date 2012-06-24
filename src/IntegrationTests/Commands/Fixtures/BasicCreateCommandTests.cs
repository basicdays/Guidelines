using System.Linq;
using Guidelines.Core.Commands;
using NUnit.Framework;
using Guidelines.Core.Commands.GuidExtensions;

namespace Guidelines.IntegrationTests.Commands.Fixtures
{
	public class BasicCreateCommandTests : CrudCommandTest
	{
		protected TestEntity RepositoryEntity { get; set; }
		protected QueryResult<TestEntity> Result { get; set; }

		protected const string TestName = "Ted";

		public class Create : ICreateCommand<TestEntity>
		{
			public string Name { get; set; }
		}

		public override void SetupTestContext()
		{
			var createCommandProcessor = Container.GetInstance<IQueryProcessor<Create, TestEntity>>();

			var createCommand = new Create { Name = TestName };

			Result = createCommandProcessor.Process(createCommand);
			RepositoryEntity = Repository.GetAll().FirstOrDefault();
		}

		[Test]
		public void TheCreateCommandIsSuccesful()
		{
			Assert.That(Result.Successful, Is.True, Result.Successful ? string.Empty : Result.Error.Message);
		}

		[Test]
		public void TheResultingEntityHasTheCorrectName()
		{
			Assert.That(Result.Result.Name, Is.EqualTo(TestName));
		}

		[Test]
		public void TheResultingEntityIsTheSameAsWhatIsInTheRepository()
		{
			Assert.That(Result.Result, Is.EqualTo(RepositoryEntity));
		}

		[Test]
		public void TheUnitOfWorkWasCommited()
		{
			Assert.That(TestUnitOfWork.Commited, Is.True);
		}
	}
}