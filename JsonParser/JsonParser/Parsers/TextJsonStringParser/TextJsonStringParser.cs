using System.Text.Json;

namespace JsonParser.Parsers.TextJsonStringParser
{
    /// <summary>
    /// Wraps System.Text.Json for string parsing.
    /// </summary>
    internal class TextJsonStringParser : IStringParser
    {
        #region "IStringParser"
        public string Serialize<TValue>(TValue pValue)
        {
            return JsonSerializer.Serialize(pValue);
        }

        public TValue? Deserialize<TValue>(string pJson)
        {
            return JsonSerializer.Deserialize<TValue>(pJson);
        }

        public ICollection<TValue> DeserializeArray<TValue>(string pJson)
        {
            return JsonSerializer.Deserialize<ICollection<TValue>>(pJson) ?? [];
        }
        #endregion
    }
}
