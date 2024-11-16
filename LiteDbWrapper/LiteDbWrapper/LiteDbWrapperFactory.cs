using LogWrapper.Loggers;
using LogWrapper.Loggers.Log4Net.ColorConsole;

using LiteDbWrapper.Wrappers;
using LiteDbWrapper.Wrappers.SimpleLiteDbWrapper;
using LiteDB;

namespace LiteDbWrapper
{
	public static class LiteDbWrapperFactory
	{
		#region "ILiteDbWrapperFactory"
		public static ILiteDbWrapper CreateNewWrapper(string pDbPath, ILoggerFactory? pCustomLogger)
		{
			if (pCustomLogger != null)
			{
				ILogger customLogger = pCustomLogger.CreateNewLogger(typeof(SimpleLiteDbWrapper));
				return new SimpleLiteDbWrapper(pDbPath, customLogger);
			}
			ILogger logger = new ColorConsoleLoggerFactory().CreateNewLogger(typeof(SimpleLiteDbWrapper));
			return new SimpleLiteDbWrapper(pDbPath, logger);
		}
		#endregion

		public static ILiteDbWrapper CreateNewWrapper(ILiteDatabase pDb, ILogger pLogger)
		{
			return new SimpleLiteDbWrapper(pDb, pLogger);
		}
	}
}
