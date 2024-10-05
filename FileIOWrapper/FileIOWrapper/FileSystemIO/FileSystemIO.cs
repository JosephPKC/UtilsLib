namespace FileIOWrapper.FileSystemIO
{
	/// <summary>
	/// Wraps System.IO.File.
	/// </summary>
	internal class FileSystemIO : IFileIO
	{
		#region "IFileIO"
		public string ReadAllText(string pFilePath)
		{
			return File.ReadAllText(pFilePath);
		}

		public void WriteAllText(string pFilePath, string? pContents)
		{
			File.WriteAllText(pFilePath, pContents);
		}
		#endregion
	}
}
