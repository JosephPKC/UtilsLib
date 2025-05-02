namespace LogWrapper.Loggers.Log4Net.ColorConsole
{
    internal class ColorConsoleLoggerFactory : ILoggerFactory
    {
        #region ILoggerFactory
        public ILogger CreateNewLogger(Type pDeclaringType)
        {
            return new ColorConsoleLogger(pDeclaringType);
        }

        public ILogger CreateNewLogger(Type pDeclaringType, LogLevels pLogLevel)
        {
            ColorConsoleLogger logger = new(pDeclaringType)
            {
                LogLevel = pLogLevel
            };
            return logger;
        }
        #endregion
    }
}