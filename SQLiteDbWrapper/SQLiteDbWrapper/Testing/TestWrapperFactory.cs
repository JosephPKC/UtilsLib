using System.Data;
using LogWrapper.Loggers;
using SqliteDbWrapper.Cache;
using SqliteDbWrapper.Wrappers;
using SqliteDbWrapper.Wrappers.LoggedWrapper;
using SqliteDbWrapper.Wrappers.SimpleWrapper;

namespace SqliteDbWrapper.Testing
{
    /// <summary>
    /// For testing purposes only.
    /// </summary>
    public static class TestWrapperFactory
    {
        public static ISqliteDbWrapper<TModel> GetSimpleWrapper<TModel>(IDbConnection pDbConnection, ISqliteDbCache<ICollection<TModel>> pCache, ILogger pLogger)
        {
            return new SimpleSqliteDbWrapper<TModel>(pDbConnection, pCache, pLogger);
        }

        public static ISqliteDbWrapper<TModel> GetLoggedWrapper<TModel>(IDbConnection pDbConnection, ISqliteDbCache<ICollection<TModel>> pCache, ILogger pLogger)
        {
            SimpleSqliteDbWrapper<TModel> wrapper = (SimpleSqliteDbWrapper<TModel>)GetSimpleWrapper(pDbConnection, pCache, pLogger);
            return new LoggedSqliteDbWrapper<TModel>(wrapper, pLogger);
        }
    }
}

