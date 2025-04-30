namespace FileIOWrapper.NullFile
{
	internal static class NullFileIOFactory
	{
		public static IFileIO CreateNewFileIO()
		{
			return new NullFileIO();
		}
	}
}
