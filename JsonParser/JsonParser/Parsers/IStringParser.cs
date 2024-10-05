namespace JsonParser.Parsers
{
	public interface IStringParser
	{
		string Serialize<TValue>(TValue pValue);
		TValue? Deserialize<TValue>(string pJson);
		ICollection<TValue> DeserializeArray<TValue>(string pJson);
	}
}
