using Cache;
using LogWrapper.Loggers;

namespace SqliteDbWrapper
{
	/// <summary>
	/// Configurations that allow the client to customize wrapper settings.
	/// </summary>
	public class SqliteDbWrapperConfigs
	{
		public ICacheFactory? CustomCache { get; set; } = null;
		public ILoggerFactory? CustomLogger {  get; set; } = null;

		/// <summary>
		/// Adds additional logs for SQLite transactions and commands, including operation time elapsed.
		/// </summary>
		public bool IsUseExtensiveLogging { get; set; } = false;
	}
}
