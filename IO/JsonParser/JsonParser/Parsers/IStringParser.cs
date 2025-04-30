namespace JsonParser.Parsers
{
	public interface IStringParser
	{
        TValue? Deserialize<TValue>(string pJson);
        IEnumerable<TValue> DeserializeArray<TValue>(string pJson);
        string Serialize<TValue>(TValue pValue);
	}
}
