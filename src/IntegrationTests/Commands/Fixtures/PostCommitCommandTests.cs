using System;
using Guidelines.Core.Commands;
using NUnit.Framework;

namespace Guidelines.IntegrationTests.Commands.Fixtures
{
	public class PostCommitCommandTests : CrudCommandTest
	{
		protected const string PostCommitName = "Placed_Here_By_Post_Commit_Command";
		protected QueryResult<TestEntity> Result { get; set; }

		public class AfterCommitCommand 
		{
			public AfterCommitCommand(TestEntity testEntity)
			{
				Entity = testEntity;
			}

			public TestEntity Entity { get; private set; }
		}

		public class AfterCommitCommandHandler : ICommandHandler<AfterCommitCommand>
		{
			public void Execute(AfterCommitCommand commandMessage)
			{
				commandMessage.Entity.Name = PostCommitName;
			}
		}

		public class NormalQuery { }

		public class NormalQueryHandler : IQueryHandler<NormalQuery, TestEntity>
		{
			private readonly IPostCommitCommandRegistrar _postCommitCommandRegistar;

			public NormalQueryHandler(IPostCommitCommandRegistrar postCommitCommandRegistar)
			{
				_postCommitCommandRegistar = postCommitCommandRegistar;
			}

			public TestEntity Execute(NormalQuery commandMessage)
			{
				var testEntity = new TestEntity
				{
					Id = Guid.NewGuid(),
					Name = string.Empty
				};

				_postCommitCommandRegistar.RegisterPostCommitCommand(new AfterCommitCommand(testEntity));

				return testEntity;
			}
		}

		public override void SetupTestContext()
		{
			var normalQueryProcessor = Container.GetInstance<IQueryProcessor<NormalQuery, TestEntity>>();

			var createCommand = new NormalQuery();

			Result = normalQueryProcessor.Process(createCommand);
		}

		[Test]
		public void TheCommandHandlerSucceded()
		{
			Assert.That(Result.Successful, Is.True);
		}

		[Test]
		public void ThePostCommitCommandSetTheEntitiesName()
		{
			Assert.That(Result.Result.Name, Is.EqualTo(PostCommitName));
		}
	}
}