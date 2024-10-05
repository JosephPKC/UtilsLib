using FileIOWrapper;

namespace JsonParser.Test.Fakes
{
	internal class TestArrayFileIO<TValue>(ICollection<TValue> pValues) : IFileIO
	{
		private ICollection<TValue> _values = pValues ?? [];

		#region "IFileIO"
		public string ReadAllText(string pFilePath)
		{
			return $"[{string.Join(",", pValues)}]";
		}

		public void WriteAllText(string pFilePath, string? pContents)
		{
			// Do Nothing
		}
		#endregion
	}
}
