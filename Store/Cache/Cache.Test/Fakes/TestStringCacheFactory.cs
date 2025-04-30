namespace Cache.Test.Fakes
{
    internal static class TestStringCacheFactory
    {
        public static ICache<string, TVal> CreateEmptyCache<TVal>()
        {
            ICacheFactory factory = CacheFacFactory.CreateStringCacheFactory();
            return factory.BuildCache<string, TVal>();
        }

        public static ICache<string, TVal> CreateLoadedCache<TVal>(string pKey, TVal pVal)
        {
            ICacheFactory factory = CacheFacFactory.CreateStringCacheFactory();
            ICache<string, TVal> cache = factory.BuildCache<string, TVal>();
            cache.Put(pKey, pVal);
            return cache;
        }
    }
}
