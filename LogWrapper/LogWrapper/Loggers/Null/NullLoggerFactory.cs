
namespace LogWrapper.Loggers.Null
{
	/// <summary>
	/// Constructs a NullLogger.
	/// </summary>
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
