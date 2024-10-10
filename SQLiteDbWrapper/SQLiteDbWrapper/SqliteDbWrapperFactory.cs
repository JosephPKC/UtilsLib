using System.Data;
using System.Data.SQLite;

using LogWrapper;
using LogWrapper.Loggers;

using SqliteDbWrapper.Cache;
using SqliteDbWrapper.Cache.SimpleCache;
using SqliteDbWrapper.Wrappers;
using SqliteDbWrapper.Wrappers.LoggedWrapper;
using SqliteDbWrapper.Wrappers.SimpleWrapper;


namespace SqliteDbWrapper
{
	/// <summary>
	/// Constructs the SqliteDbWrapper for the client.
	/// Clients should use this to construct the wrapper for use. They can supply a custom cache or configs if they want. If not, it will create a default wrapper with a default cache.
	/// </summary>
	public class SqliteDbWrapperFactory : ISqliteDbWrapperFactory
	{
		#region "ISqliteDbWrapperFactory"
		public ISqliteDbWrapper<TBaseDbModel> CreateNewWrapper<TBaseDbModel>(string pDbFilePath, bool pIsNew, SqliteDbWrapperConfigs? pConfigs = null)
		{
			if (pConfigs == null)
			{
				return CreateSimpleWrapper<TBaseDbModel>(pDbFilePath, pIsNew, pConfigs);
			}

			if (pConfigs.IsUseExtensiveLogging)
			{
				return CreateLoggedWrapper<TBaseDbModel>(pDbFilePath, pIsNew, pConfigs);
			}

			return CreateSimpleWrapper<TBaseDbModel>(pDbFilePath, pIsNew, pConfigs);
		}

		public ISqliteDbReader<TBaseDbModel> CreateNewReader<TBaseDbModel>(string pDbFilePath, bool pIsNew, SqliteDbWrapperConfigs? pConfigs = null)
		{
			if (pConfigs == null)
			{
				return CreateSimpleReader<TBaseDbModel>(pDbFilePath, pIsNew, pConfigs);
			}

			if (pConfigs.IsUseExtensiveLogging)
			{
				return CreateLoggedReader<TBaseDbModel>(pDbFilePath, pIsNew, pConfigs);
			}

			return CreateSimpleReader<TBaseDbModel>(pDbFilePath, pIsNew, pConfigs);
		}

		public ISqliteDbWriter CreateNewWriter(string pDbFilePath, bool pIsNew, SqliteDbWrapperConfigs? pConfigs = null)
		{
			if (pConfigs == null)
			{
				return CreateSimpleWriter(pDbFilePath, pIsNew, pConfigs);
			}

			if (pConfigs.IsUseExtensiveLogging)
			{
				return CreateLoggedWriter(pDbFilePath, pIsNew, pConfigs);
			}

			return CreateSimpleWriter(pDbFilePath, pIsNew, pConfigs);
		}
		#endregion

		private static SimpleSqliteDbWrapper<TBaseDbModel> CreateSimpleWrapper<TBaseDbModel>(string pDbFilePath, bool pIsNewDb, SqliteDbWrapperConfigs? pConfigs = null)
		{
			ISqliteDbCache<ICollection<TBaseDbModel>> cache = CreateCache<TBaseDbModel>(pConfigs);
			ILogger logger = CreateLogger(typeof(SimpleSqliteDbWrapper<TBaseDbModel>), pConfigs);
			IDbConnection sqlite = new SQLiteConnection($"Data Source={pDbFilePath};New={pIsNewDb}");

			return new SimpleSqliteDbWrapper<TBaseDbModel>(sqlite, cache, logger);
		}

		private static SimpleSqliteDbReader<TBaseDbModel> CreateSimpleReader<TBaseDbModel>(string pDbFilePath, bool pIsNewDb, SqliteDbWrapperConfigs? pConfigs = null)
		{
			ISqliteDbCache<ICollection<TBaseDbModel>> cache = CreateCache<TBaseDbModel>(pConfigs);
			ILogger logger = CreateLogger(typeof(SimpleSqliteDbReader<TBaseDbModel>), pConfigs);
			IDbConnection sqlite = new SQLiteConnection($"Data Source={pDbFilePath};New={pIsNewDb}");

			return new SimpleSqliteDbReader<TBaseDbModel>(sqlite, cache, logger);
		}

		private static SimpleSqliteDbWriter CreateSimpleWriter(string pDbFilePath, bool pIsNewDb, SqliteDbWrapperConfigs? pConfigs = null)
		{
			ILogger logger = CreateLogger(typeof(SimpleSqliteDbWriter), pConfigs);
			IDbConnection sqlite = new SQLiteConnection($"Data Source={pDbFilePath};New={pIsNewDb}");

			return new SimpleSqliteDbWriter(sqlite, logger);
		}

		private static LoggedSqliteDbWrapper<TBaseDbModel> CreateLoggedWrapper<TBaseDbModel>(string pDbFilePath, bool pIsNewDb, SqliteDbWrapperConfigs? pConfigs = null)
		{
			ISqliteDbWrapper<TBaseDbModel> wrapper = CreateSimpleWrapper<TBaseDbModel>(pDbFilePath, pIsNewDb, pConfigs);
			ILogger logger = CreateLogger(typeof(SimpleSqliteDbWrapper<TBaseDbModel>), pConfigs);

			return new LoggedSqliteDbWrapper<TBaseDbModel>(wrapper, logger);
		}

		private static LoggedSqliteDbReader<TBaseDbModel> CreateLoggedReader<TBaseDbModel>(string pDbFilePath, bool pIsNewDb, SqliteDbWrapperConfigs? pConfigs = null)
		{
			ISqliteDbReader<TBaseDbModel> reader = CreateSimpleReader<TBaseDbModel>(pDbFilePath, pIsNewDb, pConfigs);
			ILogger logger = CreateLogger(typeof(LoggedSqliteDbReader<TBaseDbModel>), pConfigs);

			return new LoggedSqliteDbReader<TBaseDbModel>(reader, logger);
		}

		private static LoggedSqliteDbWriter CreateLoggedWriter(string pDbFilePath, bool pIsNewDb, SqliteDbWrapperConfigs? pConfigs = null)
		{
			ISqliteDbWriter writer = CreateSimpleWriter(pDbFilePath, pIsNewDb, pConfigs);
			ILogger logger = CreateLogger(typeof(LoggedSqliteDbWriter), pConfigs);

			return new LoggedSqliteDbWriter(writer, logger);
		}

		private static ISqliteDbCache<ICollection<TBaseDbModel>> CreateCache<TBaseDbModel>(SqliteDbWrapperConfigs? pConfigs = null)
		{
			if (pConfigs == null || pConfigs.CustomCache == null)
			{
				return new SimpleSqliteDbCacheFactory().CreateNewCache<ICollection<TBaseDbModel>>();
			}

			return pConfigs.CustomCache.CreateNewCache<ICollection<TBaseDbModel>>();
		}

		private static ILogger CreateLogger(Type pCallingType, SqliteDbWrapperConfigs? pConfigs = null)
		{
			if (pConfigs == null || pConfigs.CustomLogger == null)
			{
				return LoggerFactory.CreateNullLogger(pCallingType);
			}

			return pConfigs.CustomLogger.CreateNewLogger(pCallingType);
		}
	}
}
