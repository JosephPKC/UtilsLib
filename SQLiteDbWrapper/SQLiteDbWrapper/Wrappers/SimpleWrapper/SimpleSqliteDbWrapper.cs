using System.Data;

using LogWrapper.Loggers;

using SqliteDbWrapper.Cache;
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
	internal sealed class SimpleSqliteDbWrapper<TBaseDbModel>(IDbConnection pDbConnection, ISqliteDbCache<ICollection<TBaseDbModel>> pCache, ILogger pLogger) : ISqliteDbWrapper<TBaseDbModel>
	{
		private readonly SimpleSqliteDbReader<TBaseDbModel> _reader = new(pDbConnection, pCache, pLogger);
		private readonly SimpleSqliteDbWriter _writer = new(pDbConnection, pLogger);

		#region "ISqliteDbWrapper"
		public void CreateTable(string pTableName, ICollection<string> pColumns)
		{
			_writer.CreateTable(pTableName, pColumns);
		}

		public void DropTable(string pTableName)
		{
			_writer.DropTable(pTableName);
		}

		public void Insert(string pTableName, SqliteDbValueList pValues, SqliteDbValueList? pColumns = null)
		{
			_writer.Insert(pTableName, pValues, pColumns);
		}

		public void InsertAll(string pTableName, ICollection<SqliteDbValueList> pValues, SqliteDbValueList? pColumns = null)
		{
			_writer.InsertAll(pTableName, pValues, pColumns);
		}

		public void Update(string pTableName, SqliteDbUpdateValues pValues, string? pWhere = null)
		{
			_writer.Update(pTableName, pValues, pWhere);
		}

		public ICollection<TBaseDbModel>? Select(string pTableName, ISqliteDbQuery pSelectQuery, ISqliteDbDataReader<TBaseDbModel> pReader, bool pIsForce = false)
		{
			return _reader.Select(pTableName, pSelectQuery, pReader, pIsForce);
		}

		public void Flush()
		{
			_reader.Flush();
		}

		public ISqliteDbReader<TBaseDbModel> ToReader()
		{
			return _reader;
		}

		public ISqliteDbWriter ToWriter()
		{
			return _writer;
		}
		#endregion
	}
}
