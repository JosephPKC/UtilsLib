using LogWrapper.Loggers;

namespace LogWrapper
{
    /// <summary>
    /// A simple container to easily pass down the declaring type to base classes.
    /// </summary>
    public class LoggerFactoryConf
    {
        public required ILoggerFactory LoggerFactory { get; set; }
        public required Type DeclaringType { get; set; }
    }
}
