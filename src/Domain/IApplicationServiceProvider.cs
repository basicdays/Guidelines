using System;
using System.Collections.Generic;

namespace Guidelines.Domain
{
    public interface IApplicationServiceProvider
    {
        IEnumerable<TService> GetServices<TService>();
    	TService GetInstance<TService>();

    	object GetInstance(Type arg);
    }
}