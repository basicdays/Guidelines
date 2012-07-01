using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.Mappers;
using Guidelines.Core;
using Guidelines.Mapping.AutoMapper;

namespace Guidelines.UnitTests
{
	public class TestContext
	{
		static TestContext()
		{
			var config = new ConfigurationStore(new TypeMapFactory(), MapperRegistry.AllMappers());
			Mapper = new MappingEngine(config);

			var mappings = GetMappingProfileTypes<CoreMarker>();
			mappings = mappings.Union(GetMappingProfileTypes<MappingAutoMapperMarker>());

			foreach (var mapping in mappings) {
				config.AddProfile((Profile)Activator.CreateInstance(mapping));
			}
		}

		private static IEnumerable<Type> GetMappingProfileTypes<TAssemlySource>()
		{
			Type[] domainTypes = typeof(TAssemlySource).Assembly.GetTypes();

			return domainTypes.Where(IsAssignableFromProfile).ToList();
		}

		private static bool IsAssignableFromProfile(Type type)
		{
			return typeof(Profile).IsAssignableFrom(type);
		}

		public static readonly IMappingEngine Mapper;
	}
}
