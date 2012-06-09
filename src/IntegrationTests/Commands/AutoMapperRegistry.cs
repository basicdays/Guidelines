using System.Collections.Generic;
using AutoMapper;
using AutoMapper.Mappers;
using Guidelines.Core;
using Guidelines.Core.Bootstrap;
using Guidelines.Mapping.AutoMapper;
using StructureMap.Configuration.DSL;

namespace Guidelines.IntegrationTests.Commands
{
	public class AutoMapperRegistry : Registry
	{
		public AutoMapperRegistry()
		{
			For<ConfigurationStore>()
				.Singleton()
				.Use<ConfigurationStore>()
				.Ctor<IEnumerable<IObjectMapper>>().Is(x => x.ConstructedBy(MapperRegistry.AllMappers))
				.Ctor<ITypeMapFactory>().Is<TypeMapFactory>();

			For<IConfigurationProvider>()
				.Use(ctx => ctx.GetInstance<ConfigurationStore>());

			For<IConfiguration>()
				.Use(ctx => ctx.GetInstance<ConfigurationStore>());

			For<IMappingEngine>().Use<MappingEngine>();
			For<IGenericMapper>().Use<GenericMapper>();
			For(typeof(IMapper<,>)).Use(typeof(Mapper<,>));

			Scan(scanner => {
				scanner.AssemblyContainingType<AutoMapperInfrastructureMarker>();
				scanner.WithDefaultConventions();
				scanner.AddAllTypesOf<Profile>();
				scanner.AddAllTypesOf<IBootstrapTask>();
			});
		}
	}
}
