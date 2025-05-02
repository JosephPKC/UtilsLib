namespace LogWrapper.Loggers.SimpleConsole
{
    internal class SimpleConsoleLoggerFactory : ILoggerFactory
    {
        #region ILoggerFactory
        public ILogger CreateNewLogger(Type pDeclaringType)
        {
            return new SimpleConsoleLogger(pDeclaringType);
        }

        public ILogger CreateNewLogger(Type pDeclaringType, LogLevels pLogLevel)
        {
            SimpleConsoleLogger logger = new(pDeclaringType)
            {
                LogLevel = pLogLevel
            };
            return logger;
        }
        #endregion
    }
}