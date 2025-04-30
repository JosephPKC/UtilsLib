using FileIOWrapper;

namespace JsonParser.Test.Fakes
{
	internal class TestArrayFileIO<TValue>(IEnumerable<TValue> pValues) : IFileIO
	{
        private readonly IEnumerable<TValue> _values = pValues;

        #region IFileIO
        public string ReadAllText(string pFilePath)
		{
			return $"[{string.Join(",", _values)}]";
		}

		public void WriteAllText(string pFilePath, string? pContents)
		{
			// Do Nothing
		}
		#endregion
	}
}
