using log4net.Appender;
using log4net.Layout;
using static log4net.Appender.ManagedColoredConsoleAppender;

namespace ColorConsoleLogger.Factories
{
    internal static class AppenderFactory
    {
        public static ManagedColoredConsoleAppender GetManagedColoredConsoleAppender(PatternLayout pLayout, IEnumerable<LevelColors> pLevels)
        {
            ManagedColoredConsoleAppender appender = new()
            {
                Layout = pLayout
            };

            foreach (LevelColors level in pLevels)
            {
                appender.AddMapping(level);
            }

            appender.ActivateOptions();
            return appender;
        }
    }
}
