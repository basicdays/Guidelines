using System;

namespace Guidelines.Core
{
	public interface ILocalizedDateConverter {
		DateTime Convert(DateTime value);
	}
}