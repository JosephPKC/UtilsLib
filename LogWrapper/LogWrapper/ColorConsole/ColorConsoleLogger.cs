using log4net.Appender;
using log4net.Layout;

namespace LogWrapper.ColorConsole
{
    /// <summary>
    /// Simple wrapper for the log4net logger.
    /// Centralizes the configuration and color codes the log levels.
    /// </summary>
    internal class ColorConsoleLogger(Type pDeclaringType) : BaseLogger(pDeclaringType), ILogger
    {
        #region "ILogger"
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
	}
}
