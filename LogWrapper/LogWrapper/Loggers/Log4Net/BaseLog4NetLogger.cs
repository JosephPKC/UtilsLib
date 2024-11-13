using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace LogWrapper.Loggers.Log4Net
{
    internal abstract class BaseLog4NetLogger : BaseLogger
    {
        protected readonly ILog _log;

        public BaseLog4NetLogger(Type pDeclaringType) : base(pDeclaringType)
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
			hierarchy.Root.RemoveAllAppenders();
			hierarchy.Root.Additivity = false;
			hierarchy.Root.AddAppender(pAppender);
			hierarchy.Configured = true;
        }
    }
}
