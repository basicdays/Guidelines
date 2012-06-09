using System;
using System.Collections.Generic;
using AutoMapper;
using Guidelines.Domain;
using Guidelines.Ioc.Bootstrap;

namespace Guidelines.AutoMapper
{
	public class AutoMapperTask : IBootstrapTask
	{
		public AutoMapperTask(IEnumerable<Profile> profiles, IConfiguration configuration, IApplicationServiceProvider serviceContainer)
		{
			_configuration = configuration;
			_autoMapperProfiles = profiles;
			_container = serviceContainer;
		}

		private readonly IConfiguration _configuration;
		private readonly IEnumerable<Profile> _autoMapperProfiles;
		private readonly IApplicationServiceProvider _container;
		

		public void Bootstrap()
		{
			_configuration.ConstructServicesUsing(_container.GetInstance);

			foreach (var profile in _autoMapperProfiles) {
				_configuration.AddProfile(profile);
			}

			_configuration.AllowNullDestinationValues = true;
		}

		public int Order
		{
			get { return 1; }
		}
	}
}