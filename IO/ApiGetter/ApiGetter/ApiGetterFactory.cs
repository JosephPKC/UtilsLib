using ApiGetter.Http;

namespace ApiGetter
{
    public static class ApiGetterFactory
    {
        public static IApiGetter CreateNewHttpGetter()
        {
            return HttpGetterFactory.CreateNewGetter();
        }
    }
}
