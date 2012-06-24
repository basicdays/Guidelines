using AutoMapper;
using Guidelines.Core;

namespace Guidelines.Mapping.AutoMapper.Resolvers
{
	public class NewIdResolver<TDomain, TId> : ValueResolver<object, TId>
		where TDomain : IIdentifiable<TId>
	{
		private readonly IIdGenerator<TDomain, TId> _idPolicy;

		public NewIdResolver(IIdGenerator<TDomain, TId> idPolicy)
		{
			_idPolicy = idPolicy;
		}

		protected override TId ResolveCore(object source)
		{
			return _idPolicy.GenerateId();
		}
	}
}