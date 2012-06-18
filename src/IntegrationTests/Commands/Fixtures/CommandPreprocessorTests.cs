using System;
using System.Linq;
using Guidelines.Core.Commands;
using NUnit.Framework;

namespace Guidelines.IntegrationTests.Commands.Fixtures
{
	public class CommandPreprocessorTests : BasicCreateCommandTests
	{
		protected static bool GenericPreprocessorRan { get; set; }
		protected static bool SpecificPreprocessorRan { get; set; }
		protected static bool BadPreprocessorRan { get; set; }

		public class CheckBeforeRun : ICreateCommand<TestEntity>
		{
			public string Name { get; set; }
		}

		public class TestCommandPreprocessorHook : ICommandPreprocessor
		{
			public void PreprocessCommand(object command)
			{
				GenericPreprocessorRan = true;
			}

			public bool CommandIsEligible(object command)
			{
				return command is CheckBeforeRun;
			}
		}

		public class NotRunCommandPreprocessor : ICommandPreprocessor
		{
			public void PreprocessCommand(object commandMessage)
			{
				BadPreprocessorRan = true;
				throw new NotImplementedException();
			}

			public bool CommandIsEligible(object command)
			{
				return false;
			}
		}

		public class SpecificCommandPreprocessor : ICommandPreprocessor<CheckBeforeRun>
		{
			public void PreprocessCommand(CheckBeforeRun command)
			{
				SpecificPreprocessorRan = true;
			}
		}

		public override void SetupTestContext()
		{
			SpecificPreprocessorRan = false;
			GenericPreprocessorRan = false;
			BadPreprocessorRan = false;

			var createCommandProcessor = Container.GetInstance<IQueryProcessor<CheckBeforeRun, TestEntity>>();

			var createCommand = new CheckBeforeRun { Name = TestName };

			Result = createCommandProcessor.Process(createCommand);
			RepositoryEntity = Repository.GetAll().FirstOrDefault();
		}

		[Test]
		public void GenericPreprocessorWasRan()
		{
			Assert.That(GenericPreprocessorRan, Is.True);
			GenericPreprocessorRan = false;
		}

		[Test]
		public void SpecificPreprocessorWasRan()
		{
			Assert.That(SpecificPreprocessorRan, Is.True);
			SpecificPreprocessorRan = false;
		}

		[Test]
		public void IlleligableCommandPreprocessorWasNotRan()
		{
			Assert.That(BadPreprocessorRan, Is.False);
			BadPreprocessorRan = false;
		}
	}
}