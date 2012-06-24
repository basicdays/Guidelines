using System;
using System.Linq;
using Guidelines.Core.Commands;
using Guidelines.Core.Commands.GuidExtensions;

namespace Guidelines.IntegrationTests.Commands.Fixtures
{
	public class SpecifiedUpdateCommand : BasicUpdateCommandTests
	{
		public class SpecificUpdate : IUpdateCommand<TestEntity>
		{
			public Guid? Id { get; set; }
			public string TheNameSorce { get; set; }
		}

		public class SpeficUpdateCommandHandler : IUpdateCommandHandler<SpecificUpdate, TestEntity>
		{
			public TestEntity Update(SpecificUpdate command, TestEntity workOn)
			{
				workOn.Name = command.TheNameSorce;
				return workOn;
			}
		}

		public override void SetupTestContext()
		{
			var id = Guid.NewGuid();
			Repository.Insert(new TestEntity { Id = id, Name = InitialTestName });

			var createCommandProcessor = Container.GetInstance<ICommandProcessor<SpecificUpdate>>();

			var createCommand = new SpecificUpdate { Id = id, TheNameSorce = UpdatedTestName };

			Result = createCommandProcessor.Process(createCommand);
			RepositoryEntity = Repository.GetAll().FirstOrDefault();
		}
	}
}