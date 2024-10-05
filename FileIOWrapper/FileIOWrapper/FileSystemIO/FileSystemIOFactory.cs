namespace FileIOWrapper.FileSystemIO
{
	public class FileSystemIOFactory : IFileIOFactory
	{
		#region "IFileIOFactory"
		public IFileIO CreateNewFileIO()
		{
			return new FileSystemIO();
		}
		#endregion
	}
}
