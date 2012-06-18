using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Guidelines.Core.Bootstrap;
using Guidelines.Core.Commands;
using Guidelines.Mapping.AutoMapper.Resolvers;

namespace Guidelines.Mapping.AutoMapper
{
	public class DefaultMappingsLoaderTask : IBootstrapTask
	{
		private readonly IEnumerable<IRegisterMappings> _mappingsToRegister;
		private readonly ConfigurationStore _configuration;
		private readonly bool _generateKeys;

		public DefaultMappingsLoaderTask(ConfigurationStore configuration, IEnumerable<IRegisterMappings> mappingsToRegister, bool generateKeys = true)
		{
			_mappingsToRegister = mappingsToRegister;
			_configuration = configuration;
			_generateKeys = generateKeys;
		}

		public bool HadDefaultKeyGeneration(IRegisterMappings mappingToRegiseter)
		{
			return mappingToRegiseter.KeyGenerationMethod == KeyGenerationMethod.Default;
		}

		public bool HasNoKeyGeneration(IRegisterMappings mappingToRegiseter)
		{
			return mappingToRegiseter.KeyGenerationMethod == KeyGenerationMethod.None;
		}

		public bool GeneratesKey(IRegisterMappings mappingToRegiseter)
		{
			return mappingToRegiseter.KeyGenerationMethod == KeyGenerationMethod.Generate;
		}

		public Action<IMemberConfigurationExpression> GenerateKey
		{
			get { return opt => opt.ResolveUsing<NewGuidIdResolver>(); }
		}

		public Action<IMemberConfigurationExpression> IgnoreKey
		{
			get { return opt => opt.Ignore(); }
		}

		public Action<IMemberConfigurationExpression> DefaultKeyGenerationMethod
		{
			get { return _generateKeys ? GenerateKey : IgnoreKey; }
		}

		public void Bootstrap()
		{
			var mappingsByDestination = _mappingsToRegister.GroupBy(toRegister => toRegister.DestinationType);

			foreach (IGrouping<Type, IRegisterMappings> mappingGroup in mappingsByDestination) {
				var mappingLoader = new TypeMappingLoader(mappingGroup.Key, _configuration);

				mappingLoader.CreateMappingConfigurations(mappingGroup.Where(HasNoKeyGeneration), IgnoreKey);
				mappingLoader.CreateMappingConfigurations(mappingGroup.Where(GeneratesKey), GenerateKey);
				mappingLoader.CreateMappingConfigurations(mappingGroup.Where(HadDefaultKeyGeneration), DefaultKeyGenerationMethod);
			}
		}

		public int Order
		{
			get { return 2; }
		}
	}
}
