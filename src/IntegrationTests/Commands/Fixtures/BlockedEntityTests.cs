using System;
using System.Security;
using Guidelines.Core;
using Guidelines.Core.Commands;
using NUnit.Framework;

namespace Guidelines.IntegrationTests.Commands.Fixtures
{
	public class BlockedEntityTests : CrudCommandTest
	{
		private ICommandProcessor<BlockedCommand> _commandProcessor;
		private CommandResult Result { get; set; }

		public class DeniedEntity : EntityBase<DeniedEntity>{ }

		public class BlockedCommand : IUpdateCommand<DeniedEntity>
		{
			public Guid? Id { get; set; }
		}

		public class BlockCommandPrivilage : IPermision<DeniedEntity>
		{
			public bool CanWorkOn(DeniedEntity entity)
			{
				return false;
			}
		}

		public override void SetupTestContext()
		{
			_commandProcessor = Container.GetInstance<ICommandProcessor<BlockedCommand>>();
			Result = _commandProcessor.Process(new BlockedCommand());
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

		[Test]
		public void TheUnitOfWorkWasRolledBack()
		{
			Assert.That(TestUnitOfWork.RolledBack, Is.True);
		}
	}
}