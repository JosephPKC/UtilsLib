namespace JsonParser.Parsers
{
	public interface IFileParser
	{
		void SerializeToFile<TData>(TData pObj, string pFilePath);
		TData? DeserializeFromFile<TData>(string pFilePath);
		ICollection<TData> DeserializeArrayFromFile<TData>(string pFilePath);
	}
}
