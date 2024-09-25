namespace LogWrapper.Loggers
{
    /// <summary>
    /// Logger exposes core functionality to clients.
    /// </summary>
    public interface ILogger
    {
        void Debug(object pMessage);
        void Error(object pMessage);
        void Fatal(object pMessage);
        void Info(object pMessage);
        void Warn(object pMessage);
    }
}
