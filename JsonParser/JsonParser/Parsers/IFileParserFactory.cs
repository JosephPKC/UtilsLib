using FileIOWrapper;

namespace JsonParser.Parsers
{
	public interface IFileParserFactory
	{
		IFileParser CreateNewParser(IFileIO pFileIO);
	}
}
