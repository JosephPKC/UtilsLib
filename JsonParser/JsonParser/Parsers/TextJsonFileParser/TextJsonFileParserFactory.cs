using FileIOWrapper;

namespace JsonParser.Parsers.TextJsonFileParser
{
	/// <summary>
	/// Constructs a TextJsonFileParser.
	/// </summary>
	public class TextJsonFileParserFactory : IFileParserFactory
	{
		#region "IFileParserFactory"
		public IFileParser CreateNewParser(IFileIO pFileIO)
		{
			return new TextJsonFileParser(pFileIO);
		}
		#endregion
	}
}
