using System.Text.Json;

using FileIOWrapper;

namespace JsonParser.Parsers.TextJsonFileParser
{
	/// <summary>
	/// Wraps System.Text.Json for file parsing.
	/// </summary>
	internal class TextJsonFileParser(IFileIO pFileIO) : IFileParser
	{
		protected readonly IFileIO _fileIO = pFileIO;

		#region "IFileParser"
		public void SerializeToFile<TValue>(TValue pValue, string pFilePath)
		{
			string json = JsonSerializer.Serialize(pValue);
			_fileIO.WriteAllText(pFilePath, json);
		}

		public TValue? DeserializeFromFile<TValue>(string pFilePath)
		{
			string text = _fileIO.ReadAllText(pFilePath);
			return JsonSerializer.Deserialize<TValue>(text);
		}

		public ICollection<TValue> DeserializeArrayFromFile<TValue>(string pFilePath)
		{
			string text = _fileIO.ReadAllText(pFilePath);
			return JsonSerializer.Deserialize<ICollection<TValue>>(text) ?? [];
		}
		#endregion
	}
}
