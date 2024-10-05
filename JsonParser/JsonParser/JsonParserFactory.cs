using FileIOWrapper;
using FileIOWrapper.NullFileIO;

using JsonParser.Parsers;
using JsonParser.Parsers.NullParser;
using JsonParser.Parsers.TextJsonFileParser;
using JsonParser.Parsers.TextJsonStringParser;

namespace JsonParser
{
	/// <summary>
	/// Allows clients to construct parsers.
	/// </summary>
	public static class JsonParserFactory
	{
		public static IFileParser CreateNullFileParser()
		{
			IFileIO nullFileIO = new NullFileIOFactory().CreateNewFileIO();
			return new NullFileParserFactory().CreateNewParser(nullFileIO);
		}

		public static IStringParser CreateNullStringParser()
		{
			return new NullStringParserFactory().CreateNewParser();
		}

		public static IFileParser CreateTextJsonFileParser(IFileIO pFileIO)
		{
			return new TextJsonFileParserFactory().CreateNewParser(pFileIO);
		}

		public static IStringParser CreateTextJsonStringParser()
		{
			return new TextJsonStringParserFactory().CreateNewParser();
		}
	}
}
