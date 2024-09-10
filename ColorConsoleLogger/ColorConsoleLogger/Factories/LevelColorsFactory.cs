using log4net.Core;
using static log4net.Appender.ManagedColoredConsoleAppender;

namespace ColorConsoleLogger.Factories
{
    internal static class LevelColorsFactory
    {
        public static LevelColors GetLevelColors(string pLevel)
        {
            return pLevel.ToUpper() switch
            {
                "DEBUG" => new()
                {
                    ForeColor = ConsoleColor.Green,
                    Level = Level.Debug
                },
                "ERROR" => new()
                {
                    ForeColor = ConsoleColor.Red,
                    BackColor = ConsoleColor.White,
                    Level = Level.Error
                },
                "FATAL" => new()
                {
                    ForeColor = ConsoleColor.Blue,
                    BackColor = ConsoleColor.White,
                    Level = Level.Fatal
                },
                "INFO" => new()
                {
                    ForeColor = ConsoleColor.White,
                    Level = Level.Info
                },
                "WARN" => new()
                {
                    ForeColor = ConsoleColor.Yellow,
                    Level = Level.Warn
                },
                _ => new(),
            };
        }

        public static ICollection<LevelColors> GetLevelColors(string[] pLevels)
        {
            ICollection<LevelColors> colors = [];
            foreach (string level in pLevels)
            {
                colors.Add(GetLevelColors(level));
            }
            return colors;
        }
    }
}
