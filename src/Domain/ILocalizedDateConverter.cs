using System;

namespace Guidelines.Domain
{
	public interface ILocalizedDateConverter {
		DateTime Convert(DateTime value);
	}
}