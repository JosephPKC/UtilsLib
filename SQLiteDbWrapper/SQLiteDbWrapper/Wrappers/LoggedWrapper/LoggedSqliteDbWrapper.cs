using System.Diagnostics;

using LogWrapper;

using SqliteDbWrapper.Queries;
using SqliteDbWrapper.Readers;
using SqliteDbWrapper.Values;

namespace SqliteDbWrapper.Wrappers.LoggedWrapper
{
	/// <summary>
	/// A decorator of the SimpleWrapper that adds additional logging and time analytics.
	/// </summary>
	/// <typeparam name="TBaseDbModel"></typeparam>
	internal sealed class LoggedSqliteDbWrapper<TBaseDbModel>(ISqliteDbWrapper<TBaseDbModel> pWrapper, ILogger pLogger) : ISqliteDbWrapper<TBaseDbModel>
	{
		private readonly ISqliteDbWrapper<TBaseDbModel> _wrapper = pWrapper;
		private readonly ILogger log = pLogger;

		#region "ISqliteDbWrapper"
		public void CreateTable(string pTableName, ICollection<string> pColumns)
		{
			log.Info($"BEGIN: CREATE TABLE {pTableName}");
			Stopwatch stopwatch = Stopwatch.StartNew();
			_wrapper.CreateTable(pTableName, pColumns);
			stopwatch.Stop();
			log.Info($"END: CREATE TABLE {pTableName}. Elapsed: {stopwatch.ElapsedMilliseconds} ms.");
		}

		public void DropTable(string pTableName)
		{
			log.Info($"BEGIN: DROP TABLE {pTableName}");
			Stopwatch stopwatch = Stopwatch.StartNew();
			_wrapper.DropTable(pTableName);
			stopwatch.Stop();
			log.Info($"END: DROP TABLE {pTableName}. Elapsed: {stopwatch.ElapsedMilliseconds} ms.");
		}

		public void Insert(string pTableName, SqliteDbValueList pValues, SqliteDbValueList? pColumns = null)
		{
			log.Info($"BEGIN: INSERT {pTableName}");
			Stopwatch stopwatch = Stopwatch.StartNew();
			_wrapper.Insert(pTableName, pValues, pColumns);
			stopwatch.Stop();
			log.Info($"END: INSERT {pTableName}. Elapsed: {stopwatch.ElapsedMilliseconds} ms.");
		}

		public void InsertAll(string pTableName, ICollection<SqliteDbValueList> pValues, SqliteDbValueList? pColumns = null)
		{
			log.Info($"BEGIN: INSERT ALL {pTableName}");
			Stopwatch stopwatch = Stopwatch.StartNew();
			_wrapper.InsertAll(pTableName, pValues, pColumns);
			stopwatch.Stop();
			log.Info($"END: INSERT ALL {pTableName}. Elapsed: {stopwatch.ElapsedMilliseconds} ms.");
		}

		public void Update(string pTableName, SqliteDbUpdateValues pValues, string? pWhere = null)
		{
			log.Info($"BEGIN: UPDATE {pTableName}");
			Stopwatch stopwatch = Stopwatch.StartNew();
			_wrapper.Update(pTableName, pValues, pWhere);
			stopwatch.Stop();
			log.Info($"END: UPDATE {pTableName}. Elapsed: {stopwatch.ElapsedMilliseconds} ms.");
		}

		public TBaseDbModel? SelectFirst(string pTableName, ISqliteDbQuery pSelectQuery, ISqliteDbReader pReader, bool pIsForce = false)
		{
			log.Info($"BEGIN: SELECT FIRST {pTableName}");
			Stopwatch stopwatch = Stopwatch.StartNew();
			TBaseDbModel? result = _wrapper.SelectFirst(pTableName, pSelectQuery, pReader, pIsForce);
			stopwatch.Stop();
			log.Info($"END: SELECT FIRST {pTableName}. Elapsed: {stopwatch.ElapsedMilliseconds} ms.");
			return result;
		}

		public ICollection<TBaseDbModel>? SelectAll(string pTableName, ISqliteDbQuery pSelectQuery, ISqliteDbReader pReader, bool pIsForce = false)
		{
			log.Info($"BEGIN: SELECT ALL {pTableName}");
			Stopwatch stopwatch = Stopwatch.StartNew();
			ICollection<TBaseDbModel>? result = _wrapper.SelectAll(pTableName, pSelectQuery, pReader, pIsForce);
			stopwatch.Stop();
			log.Info($"END: CREATE TABLE {pTableName}. Elapsed: {stopwatch.ElapsedMilliseconds} ms.");
			return result;
		}
		#endregion
	}
}
