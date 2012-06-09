using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Guidelines.Core;
using Guidelines.Core.Commands;
using Guidelines.Mapping.AutoMapper.Resolvers;

namespace Guidelines.Mapping.AutoMapper
{
	public interface IDefaultMappingsLoader 
	{
		void LoadDefaultMappingsForUnconfiguredCommands();
	}

	public class DefaultMappingsLoader<TDomain> : IDefaultMappingsLoader 
		where TDomain : EntityBase<TDomain>
	{
		private readonly IEnumerable<IUpdateCommand<TDomain>> _updateCommands;
		private readonly IEnumerable<ICreateCommand<TDomain>> _createCommands;
		private readonly ConfigurationStore _configuration;

		public DefaultMappingsLoader(ConfigurationStore configuration, IEnumerable<ICreateCommand<TDomain>> createCommands, IEnumerable<IUpdateCommand<TDomain>> updateCommands)
		{
			_updateCommands = updateCommands;
			_createCommands = createCommands;
			_configuration = configuration;
		}

		private bool TypeIsNotConfigured<TCommand>(TCommand command)
		{
			Type sourceType = command.GetType();
			var map = _configuration.FindTypeMapFor(sourceType, typeof(TDomain));

			return map == null;
		}

		private void CreateMappingConfigurations<TCommand>(IEnumerable<TCommand> commands, Action<IMemberConfigurationExpression> idExpression)
		{
			var unconfiguredCommands = commands.Where(TypeIsNotConfigured).Select(command => command.GetType());

			foreach (Type sourceType in unconfiguredCommands)
			{
				_configuration.CreateMap(sourceType, typeof(TDomain)).ForMember("Id", idExpression);
			}
		}

		public void LoadDefaultMappingsForUnconfiguredCommands()
		{
			CreateMappingConfigurations(_updateCommands, opt => opt.Ignore());
			CreateMappingConfigurations(_createCommands, opt => opt.ResolveUsing<NewGuidIdResolver>());
		}
	}
}
