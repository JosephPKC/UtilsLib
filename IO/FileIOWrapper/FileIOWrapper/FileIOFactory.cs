using FileIOWrapper.FileSystem;
using FileIOWrapper.NullFile;

namespace FileIOWrapper
{
    public static class FileIOFactory
    {
        public static IFileIO CreateNewFileSystemIO()
        {
            return FileSystemIOFactory.CreateNewFileIO();
        }

        public static IFileIO CreateNullIO()
        {
            return NullFileIOFactory.CreateNewFileIO();
        }
    }
}
