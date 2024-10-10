using System.Data;

using LogWrapper.Loggers;

using SqliteDbWrapper.Cache;
using SqliteDbWrapper.Readers;

namespace SqliteDbWrapper.Wrappers
{
	internal abstract class BaseSqliteDbReader<TBaseDbModel>(IDbConnection pSqlite, ISqliteDbCache<ICollection<TBaseDbModel>> pCache, ILogger pLog)
	{
		protected readonly IDbConnection _sqlite = pSqlite;
		protected readonly ISqliteDbCache<ICollection<TBaseDbModel>> _cache = pCache;
		protected readonly ILogger log = pLog;

		protected ICollection<TBaseDbModel> ExecuteQuery(string pQuery, ISqliteDbDataReader<TBaseDbModel> pReader, bool pIsForce = false)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(pQuery);
			log.Info($"SQLITE QUERY: {pQuery}");

			if (!pIsForce)
			{
				ICollection<TBaseDbModel>? itemsFromCache = _cache.Retrieve(pQuery);
				if (itemsFromCache != null)
				{
					return itemsFromCache;
				}
			}

			ICollection<TBaseDbModel> items;
			using IDbCommand command = _sqlite.CreateCommand();
			try
			{
				command.CommandText = pQuery;
				_sqlite.Open();
				using IDataReader reader = command.ExecuteReader();
				items = pReader.ReadAll(reader);
				// Store will either create a new entry or update the existing entry.
				_cache.Store(pQuery, items, 0);
			}
			catch
			{
				throw;
			}
			finally
			{
				_sqlite.Close();
			}

			return items;
		}
	}
}
