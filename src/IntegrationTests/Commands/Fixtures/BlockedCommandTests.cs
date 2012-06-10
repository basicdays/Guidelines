using System;
using System.Security;
using Guidelines.Core.Commands;
using NUnit.Framework;

namespace Guidelines.IntegrationTests.Commands.Fixtures
{
	public class BlockedCommandTests : CrudCommandTest
	{
		private IQueryProcessor<BlockedUpdate, TestEntity> _commandProcessor;
		private CommandResult Result { get; set; }

		public class BlockedUpdate : ICreateCommand<TestEntity>
		{
			public Guid Id { get; set; }
		}

		public class BlockCommandPrivilage : ICommandPermision<BlockedUpdate, TestEntity>
		{
			public bool CanWorkOn(TestEntity entity, BlockedUpdate command)
			{
				return false;
			}
		}

		public override void SetupTestContext()
		{
			_commandProcessor = Container.GetInstance<IQueryProcessor<BlockedUpdate, TestEntity>>();
			Result = _commandProcessor.Process(new BlockedUpdate());
		}

		[Test]
		public void TheCommandWillFailToProcess()
		{
			Assert.That(Result.Successful, Is.False);
		}

		[Test]
		public void TheErrorWasASecuirtyException()
		{
			Assert.That(Result.Error, Is.TypeOf<SecurityException>());
		}
	}
}