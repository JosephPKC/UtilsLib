using System.Data;

using LogWrapper.Loggers;

using SqliteDbWrapper.Cache;
using SqliteDbWrapper.Queries;
using SqliteDbWrapper.Readers;

namespace SqliteDbWrapper.Wrappers.SimpleWrapper
{
	/// <summary>
	/// Basic implementation of READ operations (SELECT) + Cache operations.
	/// </summary>
	internal sealed class SimpleSqliteDbReader<TBaseDbModel>(IDbConnection pSqlite, ISqliteDbCache<ICollection<TBaseDbModel>> pCache, ILogger pLog) : BaseSqliteDbReader<TBaseDbModel>(pSqlite, pCache, pLog), ISqliteDbReader<TBaseDbModel>
	{

		#region "ISqliteDbReader"
		public ICollection<TBaseDbModel>? Select(string pTableName, ISqliteDbQuery pSelectQuery, ISqliteDbDataReader<TBaseDbModel> pReader, bool pIsForce = false)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(pTableName);
			ArgumentNullException.ThrowIfNull(pSelectQuery);
			ArgumentNullException.ThrowIfNull(pReader);

			string query = pSelectQuery.BuildQuery(pTableName);
			return ExecuteQuery(query, pReader, pIsForce);
		}

		public void Flush()
		{
			_cache.Flush();
		}
		#endregion
	}
}
