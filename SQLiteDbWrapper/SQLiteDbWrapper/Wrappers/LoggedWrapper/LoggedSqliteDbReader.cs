using System.Diagnostics;

using LogWrapper.Loggers;

using SqliteDbWrapper.Queries;
using SqliteDbWrapper.Readers;

namespace SqliteDbWrapper.Wrappers.LoggedWrapper
{
	/// <summary>
	/// Decorates the SimpleDbReader with logging and time analytics.
	/// </summary>
	/// <typeparam name="TBaseDbModel"></typeparam>
	/// <param name="pReader"></param>
	/// <param name="pLog"></param>
	internal sealed class LoggedSqliteDbReader<TBaseDbModel>(ISqliteDbReader<TBaseDbModel> pReader, ILogger pLog) : ISqliteDbReader<TBaseDbModel>
	{
		private readonly ISqliteDbReader<TBaseDbModel> _reader = pReader;
		private readonly ILogger log = pLog;

		#region "ISqliteDbReader"
		public ICollection<TBaseDbModel>? Select(string pTableName, ISqliteDbQuery pSelectQuery, ISqliteDbDataReader<TBaseDbModel> pReader, bool pIsForce = false)
		{
			log.Info($"BEGIN: SELECT ALL {pTableName}");
			Stopwatch stopwatch = Stopwatch.StartNew();
			ICollection<TBaseDbModel>? result = _reader.Select(pTableName, pSelectQuery, pReader, pIsForce);
			stopwatch.Stop();
			log.Info($"END: CREATE TABLE {pTableName}. Elapsed: {stopwatch.ElapsedMilliseconds} ms.");
			return result;
		}

		public void Flush()
		{
			log.Info($"BEGIN: FLUSHING CACHE.");
			Stopwatch stopwatch = Stopwatch.StartNew();
			_reader.Flush();
			stopwatch.Stop();
			log.Info($"END: FLUSHING CACHE. Elapsed: {stopwatch.ElapsedMilliseconds} ms.");
		}
		#endregion
	}
}
