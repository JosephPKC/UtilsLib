namespace JsonParser.Parsers
{
	public interface IFileParser
	{
        TValue? DeserializeFromFile<TValue>(string pFilePath);
        IEnumerable<TValue> DeserializeArrayFromFile<TValue>(string pFilePath);
        void SerializeToFile<TValue>(TValue pValue, string pFilePath);
	}
}
