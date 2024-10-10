using SqliteDbWrapper.Queries;
using SqliteDbWrapper.Readers;

namespace SqliteDbWrapper.Wrappers
{
	/// <summary>
	/// For any READ operations, such as SELECT.
	/// </summary>
	/// <typeparam name="TBaseDbModel"></typeparam>
    public interface ISqliteDbReader<TBaseDbModel>
	{
		ICollection<TBaseDbModel>? Select(string pTableName, ISqliteDbQuery pSelectQuery, ISqliteDbDataReader<TBaseDbModel> pReader, bool pIsForce = false);
		void Flush();
	}
}
