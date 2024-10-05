namespace JsonParser.Parsers
{
	public interface IFileParser
	{
		void SerializeToFile<TValue>(TValue pValue, string pFilePath);

		TValue? DeserializeFromFile<TValue>(string pFilePath);

		ICollection<TValue> DeserializeArrayFromFile<TValue>(string pFilePath);
	}
}
