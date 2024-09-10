namespace JsonParser.Parsers
{
	public interface IStringParser
	{
		string Serialize<TData>(TData pObj);
		TData? Deserialize<TData>(string pJson);
		ICollection<TData> DeserializeArray<TData>(string pJson);
	}
}
