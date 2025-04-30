using FileIOWrapper;

using JsonParser.Parsers;
using JsonParser.Parsers.NullParser;
using JsonParser.Parsers.TextJsonParsing;

namespace JsonParser
{
	/// <summary>
	/// Allows clients to construct parsers.
	/// </summary>
	public static class JsonParserFactory
	{
		public static IFileParser CreateNullFileParser()
		{
			return NullParserFactory.CreateNewFileParser();
		}

		public static IStringParser CreateNullStringParser()
		{
			return NullParserFactory.CreateNewStringParser();
		}

		public static IFileParser CreateTextJsonFileParser(IFileIO? pFileIO = null)
		{
			IFileIO fileIO = pFileIO ?? FileIOFactory.CreateNewFileSystemIO();
            return TextJsonParserFactory.CreateNewFileParser(fileIO);
		}

		public static IStringParser CreateTextJsonStringParser()
		{
			return TextJsonParserFactory.CreateNewStringParser();
		}
	}
}
