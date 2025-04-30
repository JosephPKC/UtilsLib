namespace ApiGetter.Http
{
    internal static class HttpGetterFactory
    {
        public static IApiGetter CreateNewGetter()
        {
            return new HttpGetter();
        }
    }
}
