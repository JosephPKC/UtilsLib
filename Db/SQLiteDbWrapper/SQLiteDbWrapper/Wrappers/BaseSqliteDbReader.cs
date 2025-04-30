using System.Data;

using Cache;
using LogWrapper.Loggers;

using SqliteDbWrapper.Readers;

namespace SqliteDbWrapper.Wrappers
{
	internal abstract class BaseSqliteDbReader<TBaseDbModel>(IDbConnection pSqlite, ICache<string, ICollection<TBaseDbModel>> pCache, ILogger pLog)
	{
		protected readonly IDbConnection _sqlite = pSqlite;
		protected readonly ICache<string, ICollection<TBaseDbModel>> _cache = pCache;
		protected readonly ILogger log = pLog;

		protected ICollection<TBaseDbModel> ExecuteQuery(string pQuery, ISqliteDbDataReader<TBaseDbModel> pReader, bool pIsForce = false)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(pQuery);
			log.Debug($"SQLITE QUERY: {pQuery}");

			if (!pIsForce)
			{
				ICollection<TBaseDbModel>? itemsFromCache = _cache.Get(pQuery);
				if (itemsFromCache is not null)
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
				_cache.Put(pQuery, items, 0);
			}
			catch (Exception ex)
			{
				log.Error(ex.Message);
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
