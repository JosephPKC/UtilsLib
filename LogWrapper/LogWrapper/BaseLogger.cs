using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace LogWrapper
{
    internal abstract class BaseLogger
    {
        protected static readonly string[] _levels = ["DEBUG", "ERROR", "FATAL", "INFO", "WARN"];

        protected readonly ILog _log;

        public BaseLogger(Type pDeclaringType)
        {
            _log = LogManager.GetLogger(pDeclaringType);
            PatternLayout layout = GetPatternLayout();
            IAppender appender = GetAppender(layout);
            SetHierarchy(Level.Debug, appender);
        }

        protected abstract IAppender GetAppender(PatternLayout pLayout);

        protected virtual PatternLayout GetPatternLayout()
        {
            PatternLayout layout = new()
            {
                ConversionPattern = "%date: %level - %message%newline"
            };
            layout.ActivateOptions();
            return layout;
        }

        protected virtual void SetHierarchy(Level pDefaultLevel, IAppender pAppender)
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.Level = pDefaultLevel;
            hierarchy.Root.AddAppender(pAppender);
            hierarchy.Configured = true;
        }
    }
}
