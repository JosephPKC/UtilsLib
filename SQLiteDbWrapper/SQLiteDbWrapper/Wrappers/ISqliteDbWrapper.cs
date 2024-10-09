using SqliteDbWrapper.Queries;
using SqliteDbWrapper.Readers;
using SqliteDbWrapper.Values;

namespace SqliteDbWrapper.Wrappers
{
	/// <summary>
	/// SqliteDbWrapper encapsulates the actual Sqlite database connection.
	/// </summary>
	/// <typeparam name="TBaseDbModel"></typeparam>
	public interface ISqliteDbWrapper<TBaseDbModel>
    {
        void CreateTable(string pTableName, ICollection<string> pColumns);
        void DropTable(string pTableName);
        void Insert(string pTableName, SqliteDbValueList pValues, SqliteDbValueList? pColumns = null);
        void InsertAll(string pTableName, ICollection<SqliteDbValueList> pValues, SqliteDbValueList? pColumns = null);
        void Update(string pTableName, SqliteDbUpdateValues pValues, string? pWhere = null);
        ICollection<TBaseDbModel>? Select(string pTableName, ISqliteDbQuery pSelectQuery, ISqliteDbReader<TBaseDbModel> pReader, bool pIsForce = false);
        void Flush();
    }
}
