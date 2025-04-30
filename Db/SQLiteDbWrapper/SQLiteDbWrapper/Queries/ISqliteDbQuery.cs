namespace SqliteDbWrapper.Queries
{
	/// <summary>
	/// SqliteDbSelectQuery lets the client customize what kind of fields and functionalities are supported in SELECT queries.
	/// Additionally, it makes SELECT function signatures cleaner and shorter.
	/// </summary>
	public interface ISqliteDbQuery
	{
		string BuildQuery(string pTableName);
	}
}
