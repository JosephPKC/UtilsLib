
namespace LogWrapper.Loggers.SimpleConsole
{
    /// <summary>
    /// Constructs a SimpleConsoleLogger
    /// </summary>
    public class SimpleConsoleLoggerFactory : ILoggerFactory
    {
        #region "ILoggerFactory"
        public ILogger CreateNewLogger(Type pDeclaringType)
        {
            return new SimpleConsoleLogger(pDeclaringType);
        }
        #endregion
    }
}