using LogWrapper.Loggers;
using LogWrapper.Loggers.Log4Net.ColorConsole;
using LogWrapper.Loggers.Null;
using LogWrapper.Loggers.SimpleConsole;

namespace LogWrapper
{
    public static class LoggerFacFactory
    {
        public static ILoggerFactory CreateColorConsoleLoggerFactory()
        {
            return new ColorConsoleLoggerFactory();
        }

        public static ILoggerFactory CreateNullLoggerFactory()
        {
            return new NullLoggerFactory();
        }

        public static ILoggerFactory CreateSimpleConsoleLoggerFactory()
        {
            return new SimpleConsoleLoggerFactory();
        }
    }
}
