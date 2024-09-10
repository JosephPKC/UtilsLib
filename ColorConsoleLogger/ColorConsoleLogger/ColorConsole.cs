using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;

using ColorConsoleLogger.Factories;

using static log4net.Appender.ManagedColoredConsoleAppender;

namespace ColorConsoleLogger
{
    /// <summary>
    /// Simple wrapper for the log4net logger.
    /// Centralizes the configuration and color codes the log levels.
    /// </summary>
    public class ColorConsole
    {
        protected readonly ILog _log;

        public ColorConsole(Type pDeclaringType)
        {
            _log = LogManager.GetLogger(pDeclaringType);

            string[] levels = ["DEBUG", "ERROR", "FATAL", "INFO", "WARN"];

            PatternLayout layout = PatternLayoutFactory.GetPatternLayout();

            ICollection<IAppender> appenders = [];
            // Managed Colored Console Appender
            ICollection<LevelColors> colors = LevelColorsFactory.GetLevelColors(levels);
            appenders.Add(AppenderFactory.GetManagedColoredConsoleAppender(layout, colors));

            HierarchyFactory.SetHierarchy(Level.Debug, appenders);
        }

        public void Debug(object pMessage)
        {
            _log.Debug(pMessage);
        }

        public void Error(object pMessage)
        {
            _log.Error(pMessage);
        }

        public void Fatal(object pMessage)
        {
            _log.Fatal(pMessage);
        }

        public void Info(object pMessage)
        {
            _log.Info(pMessage);
        }

        public void Warn(object pMessage)
        {
            _log.Warn(pMessage);
        }
    }
}
