using System.Text.Json;

namespace JSONWrapper
{
    public static class JSONUtil
    {
        public static string Serialize<TData>(TData pObj)
        {
            return JsonSerializer.Serialize(pObj);
        }

        public static TData? Deserialize<TData>(string pFilePath)
        {
            string json = File.ReadAllText(pFilePath);
            return JsonSerializer.Deserialize<TData>(json);
        }
    }
}
