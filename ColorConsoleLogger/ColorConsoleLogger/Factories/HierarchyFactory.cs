using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Repository.Hierarchy;

namespace ColorConsoleLogger.Factories
{
    internal static class HierarchyFactory
    {
        public static void SetHierarchy(Level pLevel, ICollection<IAppender> pAppenders)
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.Level = pLevel;

            foreach (IAppender appender in pAppenders)
            {
                hierarchy.Root.AddAppender(appender);
            }

            hierarchy.Configured = true;
        }
    }
}
