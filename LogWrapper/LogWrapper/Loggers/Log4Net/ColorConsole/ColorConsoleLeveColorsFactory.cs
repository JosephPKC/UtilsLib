using log4net.Appender;
using log4net.Core;

namespace LogWrapper.Loggers.Log4Net.ColorConsole
{
    internal static class ColorConsoleLeveColorsFactory
    {
        public static ManagedColoredConsoleAppender.LevelColors GetLevelColors(string pLevel)
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
                    ForeColor = ConsoleColor.DarkRed,
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

        public static ICollection<ManagedColoredConsoleAppender.LevelColors> GetLevelColors(string[] pLevels)
        {
            ICollection<ManagedColoredConsoleAppender.LevelColors> colors = [];
            foreach (string level in pLevels)
            {
                colors.Add(GetLevelColors(level));
            }
            return colors;
        }
    }
}
