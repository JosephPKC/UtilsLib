using FileIOWrapper;

namespace JsonParser.Test.Fakes
{
	internal class TestFileIO<TValue>(TValue pValue) : IFileIO
	{
		private TValue _value = pValue;

		#region "IFileIO"
		public string ReadAllText(string pFilePath)
		{
			return _value?.ToString() ?? string.Empty;
		}

		public void WriteAllText(string pFilePath, string? pContents)
		{
			// Do Nothing
		}
		#endregion
	}
}
