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
				return new SimpleSqliteDbWrapper<TBaseDbModel>(pDbFilePath, pIsNew, null, null);
			}

			if (pConfigs.IsUseExtensiveLogging)
			{
				SimpleSqliteDbWrapper<TBaseDbModel> wrapper = new(pDbFilePath, pIsNew, pConfigs.CustomCache, pConfigs.CustomLogger);
				return new LoggedSqliteDbWrapper<TBaseDbModel>(wrapper, pConfigs.CustomLogger);
			}

			return new SimpleSqliteDbWrapper<TBaseDbModel>(pDbFilePath, pIsNew, pConfigs.CustomCache, pConfigs.CustomLogger);
		}
		#endregion
	}
}
