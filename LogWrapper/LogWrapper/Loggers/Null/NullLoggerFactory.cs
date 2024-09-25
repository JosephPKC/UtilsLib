
namespace LogWrapper.Loggers.Null
{
	public class NullLoggerFactory : ILoggerFactory
	{
		#region "ILoggerFactory"
		public ILogger CreateNewLogger(Type pDeclaringType)
		{
			return new NullLogger(pDeclaringType);
		}
		#endregion

	}
}
