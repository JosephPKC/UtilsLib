using StackExchange.Redis;

namespace RedisCache
{
    public static class RedisHandlerFactory
    {
        public static IRedisHandler CreateNewRedisHandler(IConnectionMultiplexer pConnMulti, string pPrefixPath)
        {
            return new RedisDbHandler(pConnMulti, pPrefixPath);
        }
    }
}
