namespace Cache.Basic
{
    internal class BasicCacheFactory : ICacheFactory
    {
        #region ICacheFactory
        public ICache<TKey, TVal> BuildCache<TKey, TVal>(long? pSizeLimit = null, int? pLifeInSec = null)
        {
            return new BasicCache<TKey, TVal>(pSizeLimit, pLifeInSec);
        }
        #endregion
    }
}
