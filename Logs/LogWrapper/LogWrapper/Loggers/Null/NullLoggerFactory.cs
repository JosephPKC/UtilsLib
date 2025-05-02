namespace LogWrapper.Loggers.Null
{
	internal class NullLoggerFactory : ILoggerFactory
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
