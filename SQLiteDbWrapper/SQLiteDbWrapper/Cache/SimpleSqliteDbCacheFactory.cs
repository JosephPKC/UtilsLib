namespace SqliteDbWrapper.Cache
{
	/// <summary>
	/// Creates and instantiates a SimpleSqliteDbCache.
	/// </summary>
	public class SimpleSqliteDbCacheFactory: ISqliteDbCacheFactory
	{
		#region "ISqliteDbCacheFactory"
		public ISqliteDbCache<TItem> CreateNewCache<TItem>()
		{
			return new SimpleSqliteDbCache<TItem>();
		}
		#endregion
	}
}
