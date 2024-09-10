using JsonParser.Models;
using System.Text.Json;

namespace JsonParser
{
    public static class JsonUtils
    {
		public static string Serialize<TData>(TData pObj)
		{
			return JsonSerializer.Serialize(pObj);
		}

		public static void SerializeToFile<TData>(TData pObj, string pFilePath)
		{
			string json = JsonSerializer.Serialize(pObj);
			File.WriteAllText(pFilePath, json);
		}

		public static TData? Deserialize<TData>(string pJson)
		{
			return JsonSerializer.Deserialize<TData>(pJson);
		}

		public static ICollection<TData> DeserializeArray<TData>(string pJson)
		{
			return JsonSerializer.Deserialize<ICollection<TData>>(pJson) ?? [];
		}

		public static TData? DeserializeFromFile<TData>(string pFilePath)
        {
            string json = File.ReadAllText(pFilePath);
			return Deserialize<TData>(json);
        }

		public static ICollection<TData> DeserializeArrayFromFile<TData>(string pFilePath)
		{
			string json = File.ReadAllText(pFilePath);
			return DeserializeArray<TData>(json) ?? [];
		}

		public static ICollection<TData> DeserializeIdModelArrayFromFile<TData>(string pFilePath, bool pIsAutoId = false) where TData : IIdModel
		{
			ICollection<TData> models = DeserializeArrayFromFile<TData>(pFilePath);
			if (pIsAutoId)
			{
				int id = 0;
				foreach (TData model in models)
				{
					model.Id = id++;
				}
			}
			return models;
		}
	}
}
