using System;
using AutoMapper;

namespace Guidelines.Mapping.AutoMapper.Resolvers
{
    public class CurrentUtcDateTimeResolver : ValueResolver<object, DateTime>
    {
        protected override DateTime ResolveCore(object source)
        {
            return DateTime.UtcNow;
        }
    }
}
