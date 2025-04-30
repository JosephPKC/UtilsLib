using FileIOWrapper;

namespace JsonParser.Parsers.TextJsonParsing
{
	/// <summary>
	/// Constructs a TextJsonFileParser.
	/// </summary>
	public static class TextJsonParserFactory
	{
		public static IFileParser CreateNewFileParser(IFileIO pFileIO)
		{
			return new TextJsonFileParser(pFileIO);
		}

        public static IStringParser CreateNewStringParser()
        {
            return new TextJsonStringParser();
        }
    }
}
