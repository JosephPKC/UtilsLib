namespace Cache.Test.Fakes
{
    internal static class TestBasicCacheFactory
    {
        public static ICache<TKey, TVal> CreateEmptyCache<TKey, TVal>()
        {
            ICacheFactory factory = CacheFacFactory.CreateBasicCacheFactory();
            return factory.BuildCache<TKey, TVal>();
        }

        public static ICache<TKey, TVal> CreateLoadedCache<TKey, TVal>(TKey pKey, TVal pVal)
        {
            ICacheFactory factory = CacheFacFactory.CreateBasicCacheFactory();
            ICache<TKey, TVal> cache = factory.BuildCache<TKey, TVal>();
            cache.Put(pKey, pVal);
            return cache;
        }
    }
}
