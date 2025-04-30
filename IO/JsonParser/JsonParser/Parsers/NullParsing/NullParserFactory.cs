namespace JsonParser.Parsers.NullParser
{
	internal static class NullParserFactory
	{
		public static IFileParser CreateNewFileParser()
		{
			return new NullParser();
		}

        public static IStringParser CreateNewStringParser()
        {
            return new NullParser();
        }
    }
}
