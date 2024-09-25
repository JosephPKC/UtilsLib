using LogWrapper.Loggers;
using SqliteDbWrapper.Cache;

namespace SqliteDbWrapper
{
	/// <summary>
	/// Configurations that allow the client to customize wrapper settings.
	/// </summary>
	public class SqliteDbWrapperConfigs
	{
		public ISqliteDbCacheFactory? CustomCache { get; set; } = null;
		public ILoggerFactory? CustomLogger {  get; set; } = null;

		/// <summary>
		/// Adds additional logs for SQLite transactions and commands, including operation time elapsed.
		/// </summary>
		public bool IsUseExtensiveLogging { get; set; } = false;
	}
}
