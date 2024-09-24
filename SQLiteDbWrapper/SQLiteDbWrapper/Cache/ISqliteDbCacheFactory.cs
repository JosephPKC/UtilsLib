namespace SqliteDbWrapper.Cache
{
	/// <summary>
	/// SqliteDbCacheFactory creates the cache, to avoid having the client create the cache directly.
	/// This allows the client to create and inject their own version of the cache to the wrapper.
	/// </summary>
	public interface ISqliteDbCacheFactory
	{
		ISqliteDbCache<TItem> CreateNewCache<TItem>();
	}
}
