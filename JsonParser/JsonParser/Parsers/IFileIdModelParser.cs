using JsonParser.Models;

namespace JsonParser.Parsers
{
	public interface IFileIdModelParser : IFileParser
	{
		ICollection<TData> DeserializeIdModelArrayFromFile<TData>(string pFilePath, bool pIsAutoId = false) where TData : IIdModel;
	}
}
