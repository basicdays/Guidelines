using System;

namespace Guidelines.Core
{
	public class GuidIdGenerator<TDomain> : IIdGenerator<TDomain, Guid?>
		where TDomain : EntityBase<TDomain>
	{
		private readonly IIdPolicy _idPolicy;

		public GuidIdGenerator(IIdPolicy idPolicy)
		{
			_idPolicy = idPolicy;
		}

		public Guid? GenerateId()
		{
			return _idPolicy.GetId();
		}

		public TDomain SetId(TDomain entity)
		{
			return _idPolicy.SetId(entity);
		}
	}
}