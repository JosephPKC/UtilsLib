using Cache.Basic;
using Cache.String;

namespace Cache
{
    public static class CacheFacFactory
    {
        public static ICacheFactory CreateBasicCacheFactory()
        {
            return new BasicCacheFactory();
        }

        public static ICacheFactory CreateStringCacheFactory()
        {
            return new StringCacheFactory();
        }
    }
}
