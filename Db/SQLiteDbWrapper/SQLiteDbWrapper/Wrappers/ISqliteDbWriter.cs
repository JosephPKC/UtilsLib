using SqliteDbWrapper.Values;

namespace SqliteDbWrapper.Wrappers
{
	/// <summary>
	/// For any WRITE operations, such as Create & Drop Table, Insert, and Update.
	/// </summary>
	public interface ISqliteDbWriter
	{
		void CreateTable(string pTableName, ICollection<string> pColumns);
		void DropTable(string pTableName);
		void Insert(string pTableName, SqliteDbValueList pValues, SqliteDbValueList? pColumns = null);
		void InsertAll(string pTableName, ICollection<SqliteDbValueList> pValues, SqliteDbValueList? pColumns = null);
		void Update(string pTableName, SqliteDbUpdateValues pValues, string? pWhere = null);
	}
}
