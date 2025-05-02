
using LogWrapper.Loggers.Log4Net.ColorConsole;

namespace LogWrapper.Loggers.Null
{
	public class NullLoggerFactory : ILoggerFactory
	{
		#region ILoggerFactory
		public ILogger CreateNewLogger(Type pDeclaringType)
		{
			return new NullLogger(pDeclaringType);
		}

        public ILogger CreateNewLogger(Type pDeclaringType, LogLevels pLogLevel)
        {
            NullLogger logger = new(pDeclaringType)
            {
                LogLevel = pLogLevel
            };
            return logger;
        }
        #endregion

    }
}
