namespace FileIOWrapper.FileSystem
{
	internal static class FileSystemIOFactory
	{
		public static IFileIO CreateNewFileIO()
		{
			return new FileSystemIO();
		}
	}
}
