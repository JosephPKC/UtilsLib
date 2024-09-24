using System.Data;
using System.Data.SQLite;

using Utils.Exceptions;

using SqliteDbWrapper.Cache;
using SqliteDbWrapper.Readers;
using SqliteDbWrapper.Values;
using SqliteDbWrapper.Queries;
using LogWrapper;

namespace SqliteDbWrapper.Wrappers.SimpleWrapper
{
	/// <summary>
	/// Basic implementation of the wrapper, and the default one.
	/// Supports basic Create Table, Drop Table, Insert, Update, and Select
	/// </summary>
	/// <typeparam name="TBaseDbModel"></typeparam>
	internal sealed class SimpleSqliteDbWrapper<TBaseDbModel> : ISqliteDbWrapper<TBaseDbModel>
	{
		private readonly IDbConnection _sqlite;
		private readonly ISqliteDbCache<ICollection<TBaseDbModel>> _cache;
		private readonly ILogger log;

		public SimpleSqliteDbWrapper(string pDbFilePath, bool pIsNewDb, ILogger pLogger)
		{
			_sqlite = new SQLiteConnection($"Data Source={pDbFilePath};New={pIsNewDb}");
			_cache = new SimpleSqliteDbCacheFactory().CreateNewCache<ICollection<TBaseDbModel>>();
			log = pLogger;

		}

		public SimpleSqliteDbWrapper(string pDbFilePath, bool pIsNewDb, ISqliteDbCacheFactory pCacheFactory, ILogger pLogger)
		{
			_sqlite = new SQLiteConnection($"Data Source={pDbFilePath};New={pIsNewDb}");
			_cache = pCacheFactory.CreateNewCache<ICollection<TBaseDbModel>>();
			log = pLogger;
		}

		public SimpleSqliteDbWrapper(IDbConnection pSqliteConnection, ISqliteDbCacheFactory pCacheFactory, ILogger pLogger)
		{
			_sqlite = pSqliteConnection;
			_cache = pCacheFactory.CreateNewCache<ICollection<TBaseDbModel>>();
			log = pLogger;
		}

		#region "ISqliteDbWrapper"
		/* Create & Drop Table */
		public void CreateTable(string pTableName, ICollection<string> pColumns)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(pTableName);
			ArgumentNullException.ThrowIfNull(pColumns);

			string query = $"CREATE TABLE {pTableName}({string.Join(",", pColumns)});";
			ExecuteNonQuery(query);
		}

		public void DropTable(string pTableName)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(pTableName);

			string query = $"DROP TABLE {pTableName};";
			ExecuteNonQuery(query);
		}

		/* Inserts */
		public void Insert(string pTableName, SqliteDbValueList pValues, SqliteDbValueList? pColumns = null)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(pTableName);
			ArgumentNullException.ThrowIfNull(pValues);

			string columns = pColumns == null ? string.Empty : pColumns.ToString();

			string query = $"INSERT INTO {pTableName}{columns} VALUES ({pValues});";
			ExecuteNonQuery(query);
		}

		public void InsertAll(string pTableName, ICollection<SqliteDbValueList> pValues, SqliteDbValueList? pColumns = null)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(pTableName);
			ArgumentNullExceptionExt.ThrowIfNullOrEmpty(pValues);

			string columns = pColumns == null ? string.Empty : pColumns.ToString();

			ICollection<string> valueRows = [];
			foreach (SqliteDbValueList valueRow in pValues)
			{
				valueRows.Add($"({valueRow})");
			}
			
			string query = $"INSERT INTO {pTableName}{columns} VALUES {string.Join(", ", valueRows)};";
			ExecuteNonQuery(query);
		}

		/* Updates */
		public void Update(string pTableName, SqliteDbUpdateValues pValues, string? pWhere = null)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(pTableName);
			ArgumentNullException.ThrowIfNull(pValues);

			string where = pWhere == null ? string.Empty : $"WHERE {pWhere}";
			
			string query = $"UPDATE {pTableName} SET {pValues}{where};";
			ExecuteNonQuery(query);
		}

		/* Selects */
		public TBaseDbModel? SelectFirst(string pTableName, ISqliteDbQuery pSelectQuery, ISqliteDbReader pReader, bool pIsForce = false)
		{
			ICollection<TBaseDbModel>? results = SelectAll(pTableName, pSelectQuery, pReader, pIsForce);
			return results != null && results.Count > 0 ? results.First() : default;
		}

		public ICollection<TBaseDbModel>? SelectAll(string pTableName, ISqliteDbQuery pSelectQuery, ISqliteDbReader pReader, bool pIsForce = false)
		{
			ArgumentNullException.ThrowIfNull(pSelectQuery);
			ArgumentNullException.ThrowIfNull(pReader);

			string query = pSelectQuery.BuildQuery(pTableName);
			return ExecuteQuery(query, pReader, pIsForce);
		}
		#endregion

		private void ExecuteNonQuery(string pQuery)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(pQuery);
			log.Info($"SQLITE QUERY: {pQuery}");

			using IDbCommand command = _sqlite.CreateCommand();
			try
			{
				command.CommandText = pQuery;
				_sqlite.Open();
				command.ExecuteNonQuery();
			}
			catch
			{
				throw;
			}
			finally
			{
				_sqlite.Close();
			}
		}

		private ICollection<TBaseDbModel> ExecuteQuery(string pQuery, ISqliteDbReader pReader, bool pIsForce = false)
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
				items = pReader.ReadAll<TBaseDbModel>(reader);
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
