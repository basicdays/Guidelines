using System.Collections.Generic;
using System.Linq;
using Guidelines.Domain.Specifications;

namespace Guidelines.Domain
{
	public class IsAccessibleWith<TDomain> : ISpecification<TDomain>
	{
		private readonly IEnumerable<IPermision<TDomain>> _permisionSet;

		public IsAccessibleWith(IEnumerable<IPermision<TDomain>> permisionSet)
		{
			_permisionSet = permisionSet;
		}

		public bool IsSatisfiedBy(TDomain entity)
		{
			return _permisionSet.All(permision => permision.CanWorkOn(entity));
		}
	}
}