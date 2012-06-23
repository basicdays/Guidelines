using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Guidelines.Core.Commands;

namespace Guidelines.Mapping.AutoMapper
{
	public class TypeMappingLoader
	{
		private readonly Type _domainType;
		private readonly Type _idType;
		private readonly ConfigurationStore _configuration;

		public TypeMappingLoader(Type domainType, Type idType, ConfigurationStore configuration)
		{
			_domainType = domainType;
			_idType = idType;
			_configuration = configuration;
		}

		private bool TypeIsNotConfigured<TCommand>(TCommand command)
		{
			Type sourceType = command.GetType();
			var map = _configuration.FindTypeMapFor(sourceType, _domainType);

			return map == null;
		}

		public void CreateMappingConfigurations(IEnumerable<IRegisterMappings> commands, Func<Type, Type, Action<IMemberConfigurationExpression>> idExpression)
		{
			var unconfiguredCommands = commands.Select(map => map.SourceType).Where(TypeIsNotConfigured);

			foreach (Type sourceType in unconfiguredCommands)
			{
				_configuration.CreateMap(sourceType, _domainType).ForMember("Id", idExpression(_domainType, _idType));
			}
		}
	}
}