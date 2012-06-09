using System.Collections.Generic;
using AutoMapper;
using AutoMapper.Mappers;
using Guidelines.AutoMapper;
using Guidelines.Domain;
using StructureMap.Configuration.DSL;

namespace Guidelines.IntegrationTests.IoC
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
				scanner.WithDefaultConventions();
				scanner.AddAllTypesOf<Profile>();
			});
		}
	}
}
