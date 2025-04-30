using System.Text.Json;

using JsonParser.Parsers;

namespace PkmApi.Test.Fakes
{
    internal class BasicParser : IStringParser
    {
        #region IStringParser
        public TObj? Deserialize<TObj>(string pJson)
        {
            return JsonSerializer.Deserialize<TObj>(pJson);
        }

        public IEnumerable<TObj> DeserializeArray<TObj>(string pJson)
        {
            return JsonSerializer.Deserialize<IEnumerable<TObj>>(pJson) ?? [];
        }

        public string Serialize<TValue>(TValue pValue)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
