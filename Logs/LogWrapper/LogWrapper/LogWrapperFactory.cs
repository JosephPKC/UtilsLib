using LogWrapper.Loggers;
using LogWrapper.Loggers.Log4Net.ColorConsole;
using LogWrapper.Loggers.Null;
using LogWrapper.Loggers.SimpleConsole;

namespace LogWrapper
{
    public static class LogWrapperFactory 
	{
		public static ILogger CreateColorConsoleLogger(Type pDeclaringType, LogLevels? pLogLevel = null)
		{
			ILogger logger = new ColorConsoleLoggerFactory().CreateNewLogger(pDeclaringType);
			if (pLogLevel is not null)
			{
				logger.LogLevel = pLogLevel.Value;
			}
			return logger;
		}

		public static ILogger CreateNullLogger(Type pDeclaringType, LogLevels? pLogLevel = null)
		{
            ILogger logger = new NullLoggerFactory().CreateNewLogger(pDeclaringType);
            if (pLogLevel is not null)
            {
                logger.LogLevel = pLogLevel.Value;
            }
            return logger;
        }

		public static ILogger CreateSimpleConsoleLogger(Type pDeclaringType, LogLevels? pLogLevel = null)
		{
            ILogger logger = new SimpleConsoleLoggerFactory().CreateNewLogger(pDeclaringType);
            if (pLogLevel is not null)
            {
                logger.LogLevel = pLogLevel.Value;
            }
            return logger;
        }
	}
}
