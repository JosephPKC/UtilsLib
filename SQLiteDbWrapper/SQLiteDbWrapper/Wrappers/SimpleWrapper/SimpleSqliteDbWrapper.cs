using System.Data;
using System.Data.SQLite;

using LogWrapper;
using LogWrapper.Loggers;
using Utils.Exceptions;

using SqliteDbWrapper.Cache;
using SqliteDbWrapper.Cache.SimpleCache;
using SqliteDbWrapper.Queries;
using SqliteDbWrapper.Readers;
using SqliteDbWrapper.Values;

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

		public SimpleSqliteDbWrapper(string pDbFilePath, bool pIsNewDb, ISqliteDbCacheFactory? pCacheFactory = null, ILoggerFactory? pLoggerFactory = null)
		{
			_sqlite = new SQLiteConnection($"Data Source={pDbFilePath};New={pIsNewDb}");

			if (pCacheFactory == null)
			{
				_cache = new SimpleSqliteDbCacheFactory().CreateNewCache<ICollection<TBaseDbModel>>();
			}
			else
			{
				_cache = pCacheFactory.CreateNewCache<ICollection<TBaseDbModel>>();
			}
			
			if (pLoggerFactory == null)
			{
				log = LoggerFactory.CreateNullLogger(typeof(SimpleSqliteDbWrapper<TBaseDbModel>));
			}
			else
			{
				log = pLoggerFactory.CreateNewLogger(typeof(SimpleSqliteDbWrapper<TBaseDbModel>));
			}
		}

		/// <summary>
		/// Lets the tester inject dependencies.
		/// </summary>
		/// <param name="pDbConnection"></param>
		/// <param name="pCache"></param>
		/// <param name="pLogger"></param>
		public SimpleSqliteDbWrapper(IDbConnection pDbConnection, ISqliteDbCache<ICollection<TBaseDbModel>> pCache, ILogger pLogger)
		{
			_sqlite = pDbConnection;
			_cache = pCache;
			log = pLogger;
		}

		#region "ISqliteDbWrapper"
		/* Create & Drop Table */
		public void CreateTable(string pTableName, ICollection<string> pColumns)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(pTableName);
			ArgumentExceptionExt.ThrowIfNullOrEmpty(pColumns);

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

			string columns = pColumns == null ? string.Empty : $" ({pColumns})";

			string query = $"INSERT INTO {pTableName}{columns} VALUES ({pValues});";
			ExecuteNonQuery(query);
		}

		public void InsertAll(string pTableName, ICollection<SqliteDbValueList> pValues, SqliteDbValueList? pColumns = null)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(pTableName);
			ArgumentExceptionExt.ThrowIfNullOrEmpty(pValues);

			string columns = pColumns == null ? string.Empty : $" ({pColumns})";

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

			string where = pWhere == null ? string.Empty : $" WHERE {pWhere}";
			
			string query = $"UPDATE {pTableName} SET {pValues}{where};";
			ExecuteNonQuery(query);
		}

		/* Selects */
		public ICollection<TBaseDbModel>? Select(string pTableName, ISqliteDbQuery pSelectQuery, ISqliteDbReader<TBaseDbModel> pReader, bool pIsForce = false)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(pTableName);
			ArgumentNullException.ThrowIfNull(pSelectQuery);
			ArgumentNullException.ThrowIfNull(pReader);

			string query = pSelectQuery.BuildQuery(pTableName);
			return ExecuteQuery(query, pReader, pIsForce);
		}

		/* Flush */
		public void Flush()
		{
			_cache.Flush();
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

		private ICollection<TBaseDbModel> ExecuteQuery(string pQuery, ISqliteDbReader<TBaseDbModel> pReader, bool pIsForce = false)
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
