namespace LogWrapper.Loggers.Log4Net.ColorConsole
{
    /// <summary>
    /// Constructs a ColorConsoleLogger
    /// </summary>
    public class ColorConsoleLoggerFactory : ILoggerFactory
    {
        #region "ILoggerFactory"
        public ILogger CreateNewLogger(Type pDeclaringType)
        {
            return new ColorConsoleLogger(pDeclaringType);
        }
        #endregion
    }
}