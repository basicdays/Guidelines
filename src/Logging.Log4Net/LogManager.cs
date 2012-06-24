using System;
using Guidelines.Core;

namespace Guidelines.Logging.Log4Net
{
	public static class LogManager
	{
		public static ILogger GetLogger(Type type)
		{
			return new Logger(type);
		}
	}
}