using LogWrapper.Loggers;
using LogWrapper.Loggers.Log4Net.ColorConsole;
using LogWrapper.Loggers.Null;
using LogWrapper.Loggers.SimpleConsole;

namespace LogWrapper
{
    /// <summary>
    /// Allows clients to construct loggers.
    /// </summary>
    public static class LogWrapperFactory 
	{
		public static ILogger CreateColorConsoleLogger(Type pDeclaringType)
		{
			return new ColorConsoleLoggerFactory().CreateNewLogger(pDeclaringType);
		}

		public static ILogger CreateNullLogger(Type pDeclaringType)
		{
			return new NullLoggerFactory().CreateNewLogger(pDeclaringType);
		}

		public static ILogger CreateSimpleConsoleLogger(Type pDeclaringType)
		{
			return new SimpleConsoleLoggerFactory().CreateNewLogger(pDeclaringType);
		}
	}
}
