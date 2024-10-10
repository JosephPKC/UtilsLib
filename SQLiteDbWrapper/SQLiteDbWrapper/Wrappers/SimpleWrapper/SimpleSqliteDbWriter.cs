using System.Data;

using LogWrapper.Loggers;
using Utils.Exceptions;

using SqliteDbWrapper.Values;

namespace SqliteDbWrapper.Wrappers.SimpleWrapper
{
	/// <summary>
	/// Basic implementation of WRITE operations (CREATE, DROP, INSERT, UPDATE).
	/// </summary>
	internal sealed class SimpleSqliteDbWriter(IDbConnection pSqlite, ILogger pLog) : BaseSqliteDbWriter(pSqlite, pLog), ISqliteDbWriter
	{
		#region "ISqliteDbReader"
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
		#endregion
	}
}
