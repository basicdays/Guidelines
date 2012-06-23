using System;
using System.Linq;
using Guidelines.Core;
using Guidelines.Core.Commands;
using NUnit.Framework;

namespace Guidelines.IntegrationTests.Commands.Fixtures
{
	public class ProtoectedCommandTests : BasicUpdateCommandTests
	{
		protected static bool CommandPrivsChecked { get; set; }
		protected static bool PrivsChecked { get; set; }

		public class ProtectedUpdate : IUpdateCommand<TestEntity>
		{
			public Guid? Id { get; set; }
			public string Name { get; set; }
		}

		public class ProtectedPrivilage : IPermision<TestEntity>
		{
			public bool CanWorkOn(TestEntity entity)
			{
				PrivsChecked = true;
				return true;
			}
		}

		public class ProtectedUpdatePrivilage : ICommandPermision<ProtectedUpdate, TestEntity>
		{
			public bool CanWorkOn(TestEntity entity, ProtectedUpdate command)
			{
				CommandPrivsChecked = true;
				return true;
			}
		}

		public override void SetupTestContext()
		{
			PrivsChecked = false;
			CommandPrivsChecked = false;
			
			var id = Guid.NewGuid();
			Repository.Insert(new TestEntity { Id = id, Name = InitialTestName });

			var createCommandProcessor = Container.GetInstance<ICommandProcessor<ProtectedUpdate>>();

			var createCommand = new ProtectedUpdate { Id = id, Name = UpdatedTestName };

			Result = createCommandProcessor.Process(createCommand);
			RepositoryEntity = Repository.GetAll().FirstOrDefault();
		}

		[Test]
		public void CommandPrivilagesWereChecked()
		{
			Assert.That(CommandPrivsChecked, Is.True);
			CommandPrivsChecked = false;
		}

		[Test]
		public void GenericPrivilagesWereChecked()
		{
			Assert.That(PrivsChecked, Is.True);
			PrivsChecked = false;
		}
	}
}