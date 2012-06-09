using System;
using System.Collections.Generic;
using Guidelines.Core;
using StructureMap;

namespace Guidelines.Ioc.StructureMap
{
    public class ApplicationServiceProvider : IApplicationServiceProvider
    {
        private readonly IContainer _container;

        public ApplicationServiceProvider(IContainer container)
        {
            _container = container;
        }

        public IEnumerable<TService> GetServices<TService>()
        {
            return _container.GetAllInstances<TService>();
        }

    	public object GetInstance(Type arg)
    	{
    		return _container.GetInstance(arg);
    	}

		public TService GetInstance<TService>()
		{
			return _container.GetInstance<TService>();
		}
    }
}