using System.Collections.Generic;
using Guidelines.Ioc.Bootstrap;

namespace Guidelines.AutoMapper
{
	public class DefaultMapperTask : IBootstrapTask
	{
		private readonly IEnumerable<IDefaultMappingsLoader> _defaulters;

		public DefaultMapperTask(IEnumerable<IDefaultMappingsLoader> defaulter)
		{
			_defaulters = defaulter;
		}

		public void Bootstrap()
		{
			foreach (IDefaultMappingsLoader defaultMappingsLoader in _defaulters) {
				defaultMappingsLoader.LoadDefaultMappingsForUnconfiguredCommands();	
			}
		}

		public int Order
		{
			get { return 2; }
		}
	}
}