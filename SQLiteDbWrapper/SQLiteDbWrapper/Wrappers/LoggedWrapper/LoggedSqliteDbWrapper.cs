using LogWrapper.Loggers;

using SqliteDbWrapper.Queries;
using SqliteDbWrapper.Readers;
using SqliteDbWrapper.Values;

namespace SqliteDbWrapper.Wrappers.LoggedWrapper
{
	/// <summary>
	/// An advanced wrapper that adds additional logging and time analytics.
	/// </summary>
	/// <typeparam name="TBaseDbModel"></typeparam>
	internal sealed class LoggedSqliteDbWrapper<TBaseDbModel>(ISqliteDbWrapper<TBaseDbModel> pWrapper, ILogger pLogger) : ISqliteDbWrapper<TBaseDbModel>
	{
		private readonly LoggedSqliteDbReader<TBaseDbModel> _reader = new(pWrapper.ToReader(), pLogger);
		private readonly LoggedSqliteDbWriter _writer = new(pWrapper.ToWriter(), pLogger);

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
