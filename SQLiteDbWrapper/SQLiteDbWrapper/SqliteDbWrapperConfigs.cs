using LogWrapper;

namespace SqliteDbWrapper
{
	/// <summary>
	/// Configurations that allow the client to customize wrapper settings.
	/// </summary>
	public class SqliteDbWrapperConfigs
	{
		/// <summary>
		/// Adds additional logs for SQLite transactions and commands, including operation time elapsed.
		/// </summary>
		public bool IsUseExtensiveLogging { get; set; } = false;

		/// <summary>
		/// Logger configs from the LogWrapper library to customimze logging settings.
		/// </summary>
		public LoggerConfigs? LoggerConfigs { get; set; } = null;
	}
}
