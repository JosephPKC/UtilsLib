using log4net.Appender;
using log4net.Layout;

namespace LogWrapper.Loggers.Log4Net.ColorConsole
{
    /// <summary>
    /// Simple wrapper for the log4net logger.
    /// Centralizes the configuration and color codes the log levels.
    /// </summary>
    internal class ColorConsoleLogger : BaseLog4NetLogger
    {
        public ColorConsoleLogger(Type pDeclaringType) : base(pDeclaringType)
        {
            SetupLogger();
        }

        #region BaseLogger
        public override void Debug(object? pMessage)
        {
            WriteToLog4Net(LogLevels.Debug, pMessage, _log.Debug);
        }

        public override void Error(object? pMessage)
        {
            WriteToLog4Net(LogLevels.Error, pMessage, _log.Error);
        }

        public override void Fatal(object? pMessage)
        {
            WriteToLog4Net(LogLevels.Fatal, pMessage, _log.Fatal);
        }

        public override void Info(object? pMessage)
        {
            WriteToLog4Net(LogLevels.Info, pMessage, _log.Info);
        }

        public override void Warn(object? pMessage)
        {
            WriteToLog4Net(LogLevels.Warn, pMessage, _log.Warn);
        }
        #endregion

        protected override IAppender GetAppender(PatternLayout pLayout)
        {
            ManagedColoredConsoleAppender appender = new()
            {
                Layout = pLayout
            };

            ICollection<ManagedColoredConsoleAppender.LevelColors> levelColors = ColorConsoleLeveColorsFactory.GetLevelColors(_levels);
            foreach (ManagedColoredConsoleAppender.LevelColors level in levelColors)
            {
                appender.AddMapping(level);
            }

            appender.ActivateOptions();
            return appender;
        }

        private void WriteToLog4Net(LogLevels pLogLevel, object? pMessage, Action<object?> pWriteLog)
        {
            WriteLog(pLogLevel, $"({_declaringType}): {pMessage}", pWriteLog);
        }
    }
}
