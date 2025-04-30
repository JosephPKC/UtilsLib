using StackExchange.Redis;

namespace RedisCache.Redis
{
    public static class RedisHandlerFactory
    {
        public static IRedisHandler CreateNewRedisHandler(IConnectionMultiplexer pConnMulti, string pPrefixPath)
        {
            return new RedisDbHandler(pConnMulti, pPrefixPath);
        }
    }
}
