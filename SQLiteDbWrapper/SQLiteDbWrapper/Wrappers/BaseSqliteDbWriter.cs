using System.Data;

using LogWrapper.Loggers;

namespace SqliteDbWrapper.Wrappers
{
	internal abstract class BaseSqliteDbWriter(IDbConnection pSqlite, ILogger pLog)
	{
		protected readonly IDbConnection _sqlite = pSqlite;
		protected readonly ILogger log = pLog;

		protected void ExecuteNonQuery(string pQuery)
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
	}
}
