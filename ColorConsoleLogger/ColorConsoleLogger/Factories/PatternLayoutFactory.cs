using log4net.Layout;

namespace ColorConsoleLogger.Factories
{
    internal static class PatternLayoutFactory
    {
        public static PatternLayout GetPatternLayout()
        {
            PatternLayout layout = new()
            {
                ConversionPattern = "%date: %level - %message%newline"
            };
            layout.ActivateOptions();
            return layout;
        }
    }
}
