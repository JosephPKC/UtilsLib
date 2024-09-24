namespace SqliteDbWrapper.Cache
{
	/// <summary>
	///	Key: The cache requires a string key. The wrapper utilizes the query string as the key to store results.
	///	Value: The cache can support an data type as its value.
	/// </summary>
	/// <typeparam name="TItem"></typeparam>
	public interface ISqliteDbCache<TItem>
	{
		TItem? Retrieve(string pKey);
		bool Store(string pKey, TItem pItem, int pLifeInS);
		bool Update(string pKey, TItem pItem, int pLifeInS);
		void Flush();
	}
}
