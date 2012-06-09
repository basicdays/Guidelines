using System;
using System.Collections.Generic;

namespace Guidelines.Core
{
    public interface IApplicationServiceProvider
    {
        IEnumerable<TService> GetServices<TService>();
    	TService GetInstance<TService>();

    	object GetInstance(Type arg);
    }
}