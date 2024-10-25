using LogWrapper.Loggers;

using LiteDbWrapper.Wrappers;

namespace LiteDbWrapper
{
	public interface ILiteDbWrapperFactory
	{
		ILiteDbWrapper CreateNewWrapper(string pDbPath, ILoggerFactory? pCustomLogger);
	}
}
