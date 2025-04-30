namespace FileIOWrapper
{
	public interface IFileIO
	{
		string ReadAllText(string pFilePath);
		void WriteAllText(string pFilePath, string? pContents);
	}
}
