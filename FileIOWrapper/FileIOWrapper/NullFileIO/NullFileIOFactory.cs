namespace FileIOWrapper.NullFileIO
{
	public class NullFileIOFactory : IFileIOFactory
	{
		#region "IFileIOFactory"
		public IFileIO CreateNewFileIO()
		{
			return new NullFileIO();
		}
		#endregion
	}
}
