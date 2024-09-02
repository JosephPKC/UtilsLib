using log4net;
using log4net.Config;

namespace ColorConsoleLogger
{
    /// <summary>
    /// Simple wrapper for the log4net logger.
    /// Centralizes the configuration to one config, and color codes the log levels.
    /// </summary>
    public class ColorConsole
    {
        protected readonly ILog _log;

        public ColorConsole(Type pDeclaringType)
        {
            _log = LogManager.GetLogger(pDeclaringType);
            XmlConfigurator.Configure();
        }

        public void Debug(object pMessage)
        {
            _log.Debug(pMessage);
        }

        public void Error(object pMessage)
        {
            _log.Error(pMessage);
        }

        public void Info(object pMessage)
        {
            _log.Info(pMessage);
        }
    }
}
