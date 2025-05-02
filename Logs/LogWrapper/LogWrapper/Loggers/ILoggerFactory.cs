namespace LogWrapper.Loggers
{
    /// <summary>
    /// Creates a logger
    /// </summary>
    public interface ILoggerFactory
    {
        ILogger CreateNewLogger(Type pDeclaringType);
        ILogger CreateNewLogger(Type pDeclaringType, LogLevels pLogLevel);
    }
}
