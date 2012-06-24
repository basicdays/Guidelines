using System.Linq;
using AutoMapper;
using Guidelines.Core.Commands;
using Guidelines.Mapping.AutoMapper.Resolvers;
using Guidelines.Core.Commands.GuidExtensions;

namespace Guidelines.IntegrationTests.Commands.Fixtures
{
	public class CreateCommandWithSpecificMapperTests : BasicCreateCommandTests
	{
		public class MappedCreate : ICreateCommand<TestEntity>
		{
			public string Name { get; set; }
			public string OtherName { get; set; }
		}

		public class TestEntityMappings : Profile
		{
			protected override void Configure()
			{
				CreateMap<MappedCreate, TestEntity>()
					.ForMember(dest => dest.Id, opt => opt.ResolveUsing<NewGuidIdResolver>())
					.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OtherName));
			}
		}

		public override void SetupTestContext()
		{
			var createCommandProcessor = Container.GetInstance<IQueryProcessor<MappedCreate, TestEntity>>();

			var createCommand = new MappedCreate { Name = string.Empty, OtherName = TestName };

			Result = createCommandProcessor.Process(createCommand);
			RepositoryEntity = Repository.GetAll().FirstOrDefault();
		}
	}
}