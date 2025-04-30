namespace LogWrapper.Loggers
{
	internal abstract class BaseLogger(Type pDeclaringType) : ILogger
	{
		protected readonly Type _declaringType = pDeclaringType;

        #region ILogger
        public LogLevels LogLevel { get; set; } = LogLevels.Info;

        public abstract void Debug(object? pMessage);
        public abstract void Error(object? pMessage);
        public abstract void Fatal(object? pMessage);
        public abstract void Info(object? pMessage);
        public abstract void Warn(object? pMessage);
        #endregion

        protected void WriteLog(LogLevels pLogLevel, object? pMessage, Action<LogLevels, object?> pWriteLog)
        {
            if (pLogLevel < LogLevel)
            {
                return;
            }

            pWriteLog(pLogLevel, pMessage);
        }

        protected void WriteLog(LogLevels pLogLevel, object? pMessage, Action<object?> pWriteLog)
        {
            if (pLogLevel < LogLevel)
            {
                return;
            }

            pWriteLog(pMessage);
        }
    }
}
