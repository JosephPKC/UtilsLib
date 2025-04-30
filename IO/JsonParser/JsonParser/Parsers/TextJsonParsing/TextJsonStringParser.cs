using System.Text.Json;

namespace JsonParser.Parsers.TextJsonParsing
{
    /// <summary>
    /// Wraps System.Text.Json for string parsing.
    /// </summary>
    internal class TextJsonStringParser : IStringParser
    {
        #region IStringParser
        public TValue? Deserialize<TValue>(string pJson)
        {
            return JsonSerializer.Deserialize<TValue>(pJson);
        }

        public IEnumerable<TValue> DeserializeArray<TValue>(string pJson)
        {
            return JsonSerializer.Deserialize<IEnumerable<TValue>>(pJson) ?? [];
        }

        public string Serialize<TValue>(TValue pValue)
        {
            return JsonSerializer.Serialize(pValue);
        }
        #endregion
    }
}
