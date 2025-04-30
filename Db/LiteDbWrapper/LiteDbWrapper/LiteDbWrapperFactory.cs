using LiteDB;

using LogWrapper.Loggers;
using LogWrapper.Loggers.Log4Net.ColorConsole;

using LiteDbWrapper.Wrappers;
using LiteDbWrapper.Wrappers.SimpleLiteDb;

namespace LiteDbWrapper
{
	public static class LiteDbWrapperFactory
	{
		public static ILiteDbWrapper CreateNewWrapper(string pDbPath, ILoggerFactory? pCustomLogger = null)
		{
			if (pCustomLogger != null)
			{
				ILogger customLogger = pCustomLogger.CreateNewLogger(typeof(SimpleLiteDbWrapper));
				return new SimpleLiteDbWrapper(pDbPath, customLogger);
			}
			ILogger logger = new ColorConsoleLoggerFactory().CreateNewLogger(typeof(SimpleLiteDbWrapper));
			return new SimpleLiteDbWrapper(pDbPath, logger);
		}

		public static ILiteDbWrapper CreateNewWrapper(ILiteDatabase pDb, ILogger pLogger)
		{
			return new SimpleLiteDbWrapper(pDb, pLogger);
		}
	}
}
