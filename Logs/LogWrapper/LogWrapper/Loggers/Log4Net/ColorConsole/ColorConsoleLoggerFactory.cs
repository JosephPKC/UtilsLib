namespace LogWrapper.Loggers.Log4Net.ColorConsole
{
    public class ColorConsoleLoggerFactory : ILoggerFactory
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