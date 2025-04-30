namespace JsonParser.Parsers.NullParser
{
	internal class NullParser : IFileParser, IStringParser
	{
        #region IFileParser
        public TValue? DeserializeFromFile<TValue>(string pFilePath)
        {
            return default;
        }

        public IEnumerable<TValue> DeserializeArrayFromFile<TValue>(string pFilePath)
        {
            return [];
        }

        public void SerializeToFile<TValue>(TValue pValue, string pFilePath)
		{
			// Do Nothing
		}
        #endregion

        #region IStringParser
        public TValue? Deserialize<TValue>(string pJson)
        {
            return default;
        }

        public IEnumerable<TValue> DeserializeArray<TValue>(string pJson)
        {
            return [];
        }

        public string Serialize<TValue>(TValue pValue)
		{
			return string.Empty;
		}
		#endregion
	}
}
