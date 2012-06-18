using System;
using System.Linq;
using Guidelines.Core.Commands;
using NUnit.Framework;

namespace Guidelines.IntegrationTests.Commands.Fixtures
{
	public class CommitHookTests : BasicCreateCommandTests
	{
		protected static bool GenericHookRan { get; set; }
		protected static bool SpecificHookRan { get; set; }
		protected static bool BadHookRan { get; set; }

		public class CheckAfterRun : ICreateCommand<TestEntity>
		{
			public string Name { get; set; }
		}

		public class GoodCommitHook : ICommitHook
		{
			public void OnSuccessfulCommit(object commandMessage)
			{
				GenericHookRan = true;
			}

			public bool CommandIsEligible(object command)
			{
				return command is CheckAfterRun;
			}
		}

		public class NotRunCommitHook : ICommitHook
		{
			public void OnSuccessfulCommit(object commandMessage)
			{
				BadHookRan = true;
				throw new NotImplementedException();
			}

			public bool CommandIsEligible(object command)
			{
				return false;
			}
		}

		public class SpecificCommitHook : ICommitHook<CheckAfterRun>
		{
			public void OnSuccessfulCommit(CheckAfterRun command)
			{
				SpecificHookRan = true;
			}
		}

		public override void SetupTestContext()
		{
			SpecificHookRan = false;
			GenericHookRan = false;
			BadHookRan = false;

			var createCommandProcessor = Container.GetInstance<IQueryProcessor<CheckAfterRun, TestEntity>>();

			var createCommand = new CheckAfterRun { Name = TestName };

			Result = createCommandProcessor.Process(createCommand);
			RepositoryEntity = Repository.GetAll().FirstOrDefault();
		}

		[Test]
		public void GenericCommitHookWasRan()
		{
			Assert.That(GenericHookRan, Is.True);
			GenericHookRan = false;
		}

		[Test]
		public void SpecificCommitHookWasRan()
		{
			Assert.That(SpecificHookRan, Is.True);
			SpecificHookRan = false;
		}

		[Test]
		public void CommandHookThatDoesNotPassEligibilityWasNotRan()
		{
			Assert.That(BadHookRan, Is.False);
			BadHookRan = false;
		}
	}
}