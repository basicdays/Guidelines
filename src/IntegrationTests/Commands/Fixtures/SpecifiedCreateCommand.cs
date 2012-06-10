using System;
using System.Linq;
using Guidelines.Core.Commands;

namespace Guidelines.IntegrationTests.Commands.Fixtures
{
	public class SpecifiedCreateCommand : BasicCreateCommandTests
	{
		public class SpecificCreate : ICreateCommand<TestEntity>
		{
			public string TheNameSorce { get; set; }
		}

		public class SpeficCreateCommandHandler : ICreateCommandHandler<SpecificCreate, TestEntity>
		{
			public TestEntity Create(SpecificCreate command)
			{
				return new TestEntity {
					Id = Guid.NewGuid(),
					Name = command.TheNameSorce
				};
			}
		}

		public override void SetupTestContext()
		{
			var createCommandProcessor = Container.GetInstance<IQueryProcessor<SpecificCreate, TestEntity>>();

			var createCommand = new SpecificCreate { TheNameSorce = TestName };

			Result = createCommandProcessor.Process(createCommand);
			RepositoryEntity = Repository.GetAll().FirstOrDefault();
		}
	}
}