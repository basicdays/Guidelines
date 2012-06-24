using System;

namespace Guidelines.Core
{
	public static class LogPortal
	{
		private static ILogger _logger;

		public static void SetLogger(ILogger logger)
		{
			_logger = logger;
		}

		private static void LogMessage(Action<ILogger> logFunction)
		{
			if(_logger != null) {
				lock (_logger)
				{
					logFunction(_logger);
				}
			}
		}

		public static void Debug(string message)
		{
			LogMessage(logger => logger.Debug(message));
		}

		public static void Debug(string message, Exception exception)
		{
			LogMessage(logger => logger.Debug(message, exception));
		}

		public static void Warning(string message)
		{
			LogMessage(logger => logger.Warn(message));
		}

		public static void Warning(string message, Exception exception)
		{
			LogMessage(logger => logger.Warn(message, exception));
		}

		public static void Error(string message)
		{
			LogMessage(logger => logger.Error(message));
		}

		public static void Error(string message, Exception exception)
		{
			LogMessage(logger => logger.Error(message, exception));
		}

		public static void Fatal(string message)
		{
			LogMessage(logger => logger.Fatal(message));
		}

		public static void Fatal(string message, Exception exception)
		{
			LogMessage(logger => logger.Fatal(message, exception));
		}
	}
}