using Microsoft.Extensions.Caching.Memory;

namespace Cache.Basic
{
    internal class BasicCache<TKey, TVal> : ICache<TKey, TVal>, IDisposable
    {
        private MemoryCache _cache;
        private readonly long _sizeLimit = 1024;
        private readonly int _defaultLifeInSec = 300;

        protected bool _disposed = false;

        public BasicCache(long? pSizeLimit = null, int? pLifeInSec = null)
        {
            if (pSizeLimit != null)
            {
                _sizeLimit = pSizeLimit.Value;
            }

            if (pLifeInSec != null)
            {
                _defaultLifeInSec = pLifeInSec.Value;
            }

            _cache = new(new MemoryCacheOptions() { SizeLimit = _sizeLimit });
        }

        #region ICache<Tkey, TVal>
        public virtual void Clear()
        {
            _cache = new MemoryCache(new MemoryCacheOptions() { SizeLimit = _sizeLimit });
        }

        public virtual bool Delete(TKey pKey)
        {
            if (!IsKeyValid(pKey))
            {
                return false;
            }

            if (!Exists(pKey))
            {
                return false;
            }

            _cache.Remove(pKey!);
            return true;
        }

        public virtual bool Exists(TKey pKey)
        {
            TVal? val = Get(pKey);
            return !EqualityComparer<TVal>.Default.Equals(val, default);
        }

        public virtual TVal? Get(TKey pKey)
        {
            if (!IsKeyValid(pKey))
            {
                return default;
            }

            return _cache.Get<TVal>(pKey!);
        }

        public virtual bool Put(TKey pKey, TVal pVal, int? pLifeInSec = null, bool pOverwrite = false)
        {
            if (!IsKeyValid(pKey))
            {
                return false;
            }

            if (Exists(pKey) && !pOverwrite)
            {
                return false;
            }

            MemoryCacheEntryOptions memOptions = new()
            {
                Size = 1,
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(pLifeInSec ?? _defaultLifeInSec)
            };

            _ = _cache.Set(pKey!, pVal, memOptions);
            return true;
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        protected virtual bool IsKeyValid(TKey pKey)
        {
            return !EqualityComparer<TKey>.Default.Equals(pKey, default);
        }

        protected virtual void Dispose(bool pDisposing)
        {
            if (_disposed)
            {
                return;
            }

            if (pDisposing)
            {
                _cache.Dispose();
            }

            _disposed = true;
        }
    }
}
