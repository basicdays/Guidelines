using System;
using System.Linq;
using AutoMapper;
using Guidelines.Core.Commands;

namespace Guidelines.IntegrationTests.Commands.Fixtures
{
	public class UpdateCommandWithSpecificMapperTests : BasicUpdateCommandTests
	{
		public class MappedUpdate : IUpdateCommand<TestEntity>
		{
			public Guid? Id { get; set; }
			public string Name { get; set; }
			public string OtherName { get; set; }
		}

		public class TestEntityMappings : Profile
		{
			protected override void Configure()
			{
				CreateMap<MappedUpdate, TestEntity>()
					.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OtherName));
			}
		}

		public override void SetupTestContext()
		{
			var id = Guid.NewGuid();
			Repository.Insert(new TestEntity { Id = id, Name = InitialTestName });

			var updateCommandProcessor = Container.GetInstance<ICommandProcessor<MappedUpdate>>();

			var updateCommand = new MappedUpdate { Id = id, Name = string.Empty, OtherName = UpdatedTestName };

			Result = updateCommandProcessor.Process(updateCommand);
			RepositoryEntity = Repository.GetAll().FirstOrDefault();
		}
	}
}