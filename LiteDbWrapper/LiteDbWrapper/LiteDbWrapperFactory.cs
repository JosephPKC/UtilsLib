using LogWrapper.Loggers;
using LogWrapper.Loggers.Log4Net.ColorConsole;

using LiteDbWrapper.Wrappers;
using LiteDbWrapper.Wrappers.SimpleLiteDbWrapper;
using LiteDB;

namespace LiteDbWrapper
{
	public class LiteDbWrapperFactory : ILiteDbWrapperFactory
	{
		#region "ILiteDbWrapperFactory"
		public ILiteDbWrapper CreateNewWrapper(string pDbPath, ILoggerFactory? pCustomLogger)
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

		public ILiteDbWrapper CreateNewWrapper(ILiteDatabase pDb, ILogger pLogger)
		{
			return new SimpleLiteDbWrapper(pDb, pLogger);
		}
	}
}
