using JsonParser.Models;

namespace JsonParser.Parsers
{
	public class JsonParser : IFileIdModelParser, IStringParser
    {
		public string Serialize<TData>(TData pObj)
		{
			return JsonUtils.Serialize(pObj);
		}

		public void SerializeToFile<TData>(TData pObj, string pFilePath)
		{
			SerializeToFile(pObj, pFilePath);
		}

		public TData? Deserialize<TData>(string pJson)
		{
			return JsonUtils.Deserialize<TData>(pJson);
		}

		public ICollection<TData> DeserializeArray<TData>(string pJson)
		{
			return JsonUtils.DeserializeArray<TData>(pJson);
		}

		public TData? DeserializeFromFile<TData>(string pFilePath)
		{
			return JsonUtils.DeserializeFromFile<TData>(pFilePath);
		}

		public ICollection<TData> DeserializeArrayFromFile<TData>(string pFilePath)
		{
			return JsonUtils.DeserializeArrayFromFile<TData>(pFilePath);
		}

		public ICollection<TData> DeserializeIdModelArrayFromFile<TData>(string pFilePath, bool pIsAutoId = false) where TData : IIdModel
		{
			return JsonUtils.DeserializeIdModelArrayFromFile<TData>(pFilePath, pIsAutoId);
		}
	}
}