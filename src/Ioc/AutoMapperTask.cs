using System.Collections.Generic;
using AutoMapper;
using Guidelines.Ioc.Bootstrap;
using Microsoft.Practices.ServiceLocation;

namespace Guidelines.Ioc
{
	public class AutoMapperTask : IBootstrapTask
	{
		public AutoMapperTask(IEnumerable<Profile> profiles, IConfiguration configuration, IServiceLocator serviceContainer)
		{
			_configuration = configuration;
			_autoMapperProfiles = profiles;
			_container = serviceContainer;
		}

		private readonly IConfiguration _configuration;
		private readonly IEnumerable<Profile> _autoMapperProfiles;
		private readonly IServiceLocator _container;

		public void Bootstrap()
		{
			_configuration.ConstructServicesUsing(_container.GetInstance);

			foreach (var profile in _autoMapperProfiles) {
				_configuration.AddProfile(profile);
			}
			_configuration.AllowNullDestinationValues = true;
		}
	}
}