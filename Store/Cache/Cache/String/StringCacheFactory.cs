namespace Cache.String
{
    internal class StringCacheFactory : ICacheFactory
    {
        #region ICacheFactory
        public ICache<TKey, TVal> BuildCache<TKey, TVal>(long? pSizeLimit = null, int? pLifeInSec = null)
        {
            return (ICache<TKey, TVal>)new StringCache<TVal>();
        }
        #endregion
    }
}
