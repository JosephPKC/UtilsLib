using Microsoft.Extensions.Caching.Memory;

namespace SqliteDbWrapper.Cache.SimpleCache
{
    /// <summary>
    /// SimpleSqliteDbCache is a very basic implementation of the cache.
    /// It is the default cache the wrapper uses if the client does not provide their own.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    internal class SimpleSqliteDbCache<TItem> : ISqliteDbCache<TItem>, IDisposable
    {
        private readonly long _defaultSizeLimit = 1024;
        private readonly int _defaultLifeInS = 300;
        private MemoryCache _cache;

        public SimpleSqliteDbCache()
        {
            _cache = new MemoryCache(new MemoryCacheOptions() { SizeLimit = _defaultSizeLimit });
        }

        public SimpleSqliteDbCache(long pSizeLimit, int pLifeInS)
        {
            _defaultSizeLimit = pSizeLimit;
            _defaultLifeInS = pLifeInS;
            _cache = new MemoryCache(new MemoryCacheOptions() { SizeLimit = _defaultSizeLimit });
        }

        #region "ISQLiteDbCacheHandler<TKey, TItem>"
        /// <summary>
        /// Returns the item associated with the key if it exists, or Default if it does not.
        /// </summary>
        /// <param name="pKey"></param>
        /// <returns></returns>
        public TItem? Retrieve(string pKey)
        {
            if (string.IsNullOrWhiteSpace(pKey))
            {
                return default;
            }

            return _cache.Get<TItem>(pKey);
        }

        /// <summary>
        /// Stores the item and its key if the key does not exist. If the operation is successful, returns true.
        /// If the key is invalid, or the key already exists, returns false.
        /// If the LifeInS is 0 or negative, it will default to the DefaultLifeInS.
        /// </summary>
        /// <param name="pKey"></param>
        /// <param name="pItem"></param>
        /// <param name="pLifeInS"></param>
        /// <returns></returns>
        public bool Store(string pKey, TItem pItem, int pLifeInS)
        {
            if (string.IsNullOrWhiteSpace(pKey))
            {
                return false;
            }

            // Do not overwrite item if it already exists.
            TItem? item = Retrieve(pKey);
            if (item != null)
            {
                return false;
            }

            MemoryCacheEntryOptions memOptions = new()
            {
                Size = 1,
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(pLifeInS > 0 ? pLifeInS : _defaultLifeInS)
            };

            _ = _cache.Set(pKey, pItem, memOptions);
            return true;
        }

        /// <summary>
        /// Updates an existing item. If successful, returns true.
        /// If the item does not exist, it will Store the item instead.
        /// If the key is invalid, returns false.
        /// If the LifeInS is 0 or negative, it will default to the DefaultLifeInS.
        /// </summary>
        /// <param name="pKey"></param>
        /// <param name="pItem"></param>
        /// <param name="pLifeInS"></param>
        /// <returns></returns>
        public bool Update(string pKey, TItem pItem, int pLifeInS)
        {
            if (string.IsNullOrWhiteSpace(pKey))
            {
                return false;
            }

            MemoryCacheEntryOptions memOptions = new()
            {
                Size = 1,
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(pLifeInS > 0 ? pLifeInS : _defaultLifeInS)
            };

            // MemoryCache.Set will create a new entry if key does not exist, or just overwrite the existing one.
            _ = _cache.Set(pKey, pItem, memOptions);
            return true;
        }

        /// <summary>
        /// Disposes of the cache completely.
        /// Creates a new cache with the size of DefaultSizeLimit.
        /// </summary>
        public void Flush()
        {
            _cache.Dispose();
            _cache = new MemoryCache(new MemoryCacheOptions() { SizeLimit = _defaultSizeLimit });
        }
        #endregion

        #region "IDisposable"
        /// <summary>
        /// Disposes of the cache completely.
        /// </summary>
        public void Dispose()
        {
            _cache.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
