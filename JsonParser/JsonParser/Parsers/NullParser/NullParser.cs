namespace JsonParser.Parsers.NullParser
{
	internal class NullParser : IFileParser, IStringParser
	{

		#region "IFileParser"
		public void SerializeToFile<TValue>(TValue pValue, string pFilePath)
		{
			// Do Nothing
		}

		public TValue? DeserializeFromFile<TValue>(string pFilePath)
		{
			return default;
		}

		public ICollection<TValue> DeserializeArrayFromFile<TValue>(string pFilePath)
		{
			return [];
		}
		#endregion

		#region "IStringParser"
		public string Serialize<TValue>(TValue pValue)
		{
			return string.Empty;
		}
		public TValue? Deserialize<TValue>(string pJson)
		{
			return default;
		}

		public ICollection<TValue> DeserializeArray<TValue>(string pJson)
		{
			return [];
		}
		#endregion
	}
}
