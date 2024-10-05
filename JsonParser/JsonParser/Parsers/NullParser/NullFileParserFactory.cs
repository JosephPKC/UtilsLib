using FileIOWrapper;

namespace JsonParser.Parsers.NullParser
{
	public class NullFileParserFactory : IFileParserFactory
	{
		#region "IFileParserFactory"
		public IFileParser CreateNewParser(IFileIO pFileIO)
		{
			return new NullParser();
		}
		#endregion
	}
}
