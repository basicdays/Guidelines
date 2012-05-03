using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Guidelines.Domain
{
	public static class EnumerableExtensions
	{
		public static bool IsEmpty(this IEnumerable collection)
		{
			return !collection.GetEnumerator().MoveNext();
		}

		public static bool IsOnly<TType>(this IEnumerable<TType> collection, TType obj)
		{
			return collection != null
				&& (collection.Count() == 1
				&& collection.First().Equals(obj));
		}
	}
}
