using System;
using AutoMapper;
using Guidelines.Domain;

namespace Guidelines.AutoMapper.Resolvers
{
    public class NewGuidIdResolver : ValueResolver<object, Guid>
    {
		private readonly IIdPolicy _idPolicy;

		public NewGuidIdResolver(IIdPolicy idPolicy)
        {
            _idPolicy = idPolicy;
        }

        protected override Guid ResolveCore(object source)
        {
            return _idPolicy.GetId();
        }
    }
}