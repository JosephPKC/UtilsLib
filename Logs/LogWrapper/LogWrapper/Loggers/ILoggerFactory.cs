namespace LogWrapper.Loggers
{
    /// <summary>
    /// Creates a logger
    /// </summary>
    public interface ILoggerFactory
    {
        ILogger CreateNewLogger(Type pDeclaringType);
    }
}
