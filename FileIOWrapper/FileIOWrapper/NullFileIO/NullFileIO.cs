namespace FileIOWrapper.NullFileIO
{
	/// <summary>
	/// Null version of the FileIO
	/// </summary>
	internal class NullFileIO : IFileIO
	{
		#region "IFileIO"
		public string ReadAllText(string pFilePath)
		{
			return string.Empty;
		}

		public void WriteAllText(string pFilePath, string? pContents)
		{
			// Do Nothing
		}
		#endregion
	}
}
