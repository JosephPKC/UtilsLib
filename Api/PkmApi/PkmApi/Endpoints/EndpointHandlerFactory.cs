using ApiGetter;
using Cache;
using JsonParser;
using JsonParser.Parsers;
using LogWrapper;
using LogWrapper.Loggers;

using PkmApi.Dtos;

namespace PkmApi.Endpoints
{
    public static class EndpointHandlerFactory
    {
        public static IEndpointHandler<TData> BuildDefaultEndpointHandler<TData>(string pBaseUri, string pVersion, string pEndpoint, IApiGetter? pApiGetter = null, IStringParser? pJsonParser = null, ILogger? pLogger = null,
            ICacheFactory? pCacheFactory = null, int? pCacheSizeLimit = null, int? pCacheLifeInSec = null
            ) where TData : class, IPkmApiDto
        {
            IApiGetter getter = pApiGetter ?? ApiGetterFactory.CreateNewHttpGetter();
            IStringParser parser = pJsonParser ?? JsonParserFactory.CreateTextJsonStringParser();
            ILogger logger = pLogger ?? LogWrapperFactory.CreateColorConsoleLogger(typeof(BasePkmEndpointHandler<TData>));
            ICacheFactory cacheFactory = pCacheFactory ?? CacheFacFactory.CreateStringCacheFactory();
            int? cacheSizeLimit = pCacheSizeLimit ?? Config.DefaultCacheSizeLimit;
            int? cacheLifeInSec = pCacheLifeInSec ?? Config.DefaultCacheLifeInSec;

            return BuildEndpointHandler<TData>(pBaseUri, pVersion, pEndpoint, getter, parser, logger, cacheFactory, cacheSizeLimit, cacheLifeInSec);
        }

        public static IEndpointHandler<TData> BuildEndpointHandler<TData>(
            string pBaseUri, string pVersion, string pEndpoint, 
            IApiGetter pApiGetter, IStringParser pJsonParser, ILogger pLogger, 
            ICacheFactory? pCacheFactory = null, int? pCacheSizeLimit = null, int? pCacheLifeInSec = null
            ) where TData : class, IPkmApiDto
        {
            return new BasePkmEndpointHandler<TData>(pBaseUri, pVersion, pEndpoint, pApiGetter, pJsonParser, pLogger, pCacheFactory, pCacheSizeLimit, pCacheLifeInSec);
        }
    }
}
