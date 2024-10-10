using System.Diagnostics;

using LogWrapper.Loggers;

using SqliteDbWrapper.Values;

namespace SqliteDbWrapper.Wrappers.LoggedWrapper
{
	/// <summary>
	/// Decorates the SimpleDbWriter with logging and time analytics.
	/// </summary>
	/// <typeparam name="TBaseDbModel"></typeparam>
	/// <param name="pReader"></param>
	/// <param name="pLog"></param>
	internal sealed class LoggedSqliteDbWriter(ISqliteDbWriter pWriter, ILogger pLog) : ISqliteDbWriter
	{
		private readonly ISqliteDbWriter _writer = pWriter;
		private readonly ILogger log = pLog;

		#region "ISqliteDbWriter"
		public void CreateTable(string pTableName, ICollection<string> pColumns)
		{
			log.Info($"BEGIN: CREATE TABLE {pTableName}");
			Stopwatch stopwatch = Stopwatch.StartNew();
			_writer.CreateTable(pTableName, pColumns);
			stopwatch.Stop();
			log.Info($"END: CREATE TABLE {pTableName}. Elapsed: {stopwatch.ElapsedMilliseconds} ms.");
		}

		public void DropTable(string pTableName)
		{
			log.Info($"BEGIN: DROP TABLE {pTableName}");
			Stopwatch stopwatch = Stopwatch.StartNew();
			_writer.DropTable(pTableName);
			stopwatch.Stop();
			log.Info($"END: DROP TABLE {pTableName}. Elapsed: {stopwatch.ElapsedMilliseconds} ms.");
		}

		public void Insert(string pTableName, SqliteDbValueList pValues, SqliteDbValueList? pColumns = null)
		{
			log.Info($"BEGIN: INSERT {pTableName}");
			Stopwatch stopwatch = Stopwatch.StartNew();
			_writer.Insert(pTableName, pValues, pColumns);
			stopwatch.Stop();
			log.Info($"END: INSERT {pTableName}. Elapsed: {stopwatch.ElapsedMilliseconds} ms.");
		}

		public void InsertAll(string pTableName, ICollection<SqliteDbValueList> pValues, SqliteDbValueList? pColumns = null)
		{
			log.Info($"BEGIN: INSERT ALL {pTableName}");
			Stopwatch stopwatch = Stopwatch.StartNew();
			_writer.InsertAll(pTableName, pValues, pColumns);
			stopwatch.Stop();
			log.Info($"END: INSERT ALL {pTableName}. Elapsed: {stopwatch.ElapsedMilliseconds} ms.");
		}

		public void Update(string pTableName, SqliteDbUpdateValues pValues, string? pWhere = null)
		{
			log.Info($"BEGIN: UPDATE {pTableName}");
			Stopwatch stopwatch = Stopwatch.StartNew();
			_writer.Update(pTableName, pValues, pWhere);
			stopwatch.Stop();
			log.Info($"END: UPDATE {pTableName}. Elapsed: {stopwatch.ElapsedMilliseconds} ms.");
		}
		#endregion
	}
}
