﻿using System.Data;

namespace SqliteDbWrapper.Readers
{
	/// <summary>
	/// The SqliteDbReader lets the client define how to translate data from SELECT queries to their own data object models.
	/// The client is REQUIRED to provide this when doing SELECT queries.
	/// </summary>
	public interface ISqliteDbReader
	{
		TBaseDbModel? ReaderFirst<TBaseDbModel>(IDataReader pReader);
		ICollection<TBaseDbModel> ReadAll<TBaseDbModel>(IDataReader pReader);
	}
}