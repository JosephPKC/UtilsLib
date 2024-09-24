using LogWrapper;
using SqliteDbWrapper.Cache;
using SqliteDbWrapper.Wrappers;
using SqliteDbWrapper.Wrappers.SimpleWrapper;

namespace SqliteDbWrapper
{
    /// <summary>
    /// Constructs the SqliteDbWrapper for the client.
	/// Clients should use this to construct the wrapper for use. They can supply a custom cache or configs if they want. If not, it will create a default wrapper with a default cache.
    /// </summary>
    public static class SqliteDbWrapperFactory
	{
		public static ISqliteDbWrapper<TBaseDbModel> CreateNewWrapper<TBaseDbModel>(string pDbFilePath, bool pIsNew, ISqliteDbCacheFactory? pCacheFactory = null, SqliteDbWrapperConfigs? pConfigs = null)
		{
			if (pConfigs == null)
			{
				return CreateDefaultWrapper<TBaseDbModel>(pDbFilePath, pIsNew, pCacheFactory);
			}

			if (pConfigs.IsUseExtensiveLogging)
			{
				return CreateSimpleWrapper<TBaseDbModel>(pDbFilePath, pIsNew, pCacheFactory, pConfigs.LoggerConfigs);
			}

			return CreateDefaultWrapper<TBaseDbModel>(pDbFilePath, pIsNew, pCacheFactory, pConfigs.LoggerConfigs);
		}

		private static SimpleSqliteDbWrapper<TBaseDbModel> CreateDefaultWrapper<TBaseDbModel>(string pDbFilePath, bool pIsNew, ISqliteDbCacheFactory? pCacheFactory = null, LoggerConfigs? pLogConfigs = null)
		{
			return CreateSimpleWrapper<TBaseDbModel>(pDbFilePath, pIsNew, pCacheFactory, pLogConfigs);	
		}

		private static SimpleSqliteDbWrapper<TBaseDbModel> CreateSimpleWrapper<TBaseDbModel>(string pDbFilePath, bool pIsNew, ISqliteDbCacheFactory? pCacheFactory = null, LoggerConfigs? pLogConfigs = null)
		{
			ILogger logger = LoggerFactory.CreateNewLogger(typeof(SimpleSqliteDbWrapper<TBaseDbModel>), pLogConfigs);

			if (pCacheFactory == null)
			{
				return new SimpleSqliteDbWrapper<TBaseDbModel>(pDbFilePath, pIsNew, logger);
			}
			else
			{
				return new SimpleSqliteDbWrapper<TBaseDbModel>(pDbFilePath, pIsNew, pCacheFactory, logger);
			}
		}
	}
}
