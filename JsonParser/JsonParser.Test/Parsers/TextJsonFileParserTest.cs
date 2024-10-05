using Moq;

using FileIOWrapper;
using FileIOWrapper.NullFileIO;

using JsonParser.Parsers;
using JsonParser.Parsers.TextJsonFileParser;

using JsonParser.Test.Fakes;

namespace JsonParser.Test.Parsers
{
	/// <summary>
	/// Tests TextJsonFileParser.
	/// </summary>
	public class TextJsonFileParserTest
	{
		public static IFileParser CreateFileParser(IFileIO? pFileIO = null)
		{
			if (pFileIO == null)
			{
				IFileIO fileIO = new NullFileIOFactory().CreateNewFileIO();
				return new TextJsonFileParserFactory().CreateNewParser(fileIO);
			}

			return new TextJsonFileParserFactory().CreateNewParser(pFileIO);
		}

		#region "SerializeToFile"
		[Fact]
		public void Test_SerializeToFile_WriteAllText()
		{
			// Arrange
			Mock<IFileIO> mockFileIO = new();
			IFileParser parser = CreateFileParser(mockFileIO.Object);
			int value = 1;
			string filePath = "test";

			// Act
			parser.SerializeToFile(value, filePath);

			// Assert
			mockFileIO.Verify(x => x.WriteAllText(It.IsAny<string>(), It.IsAny<string?>()), Times.Once());
		}
		#endregion

		#region "DeserializeFromFile"
		[Fact]
		public void Test_DeserializeFromFile_ReturnDeserializedValue()
		{
			// Arrange
			int value = 1;
			TestFileIO<int> testFileIO = new(value);
			IFileParser parser = CreateFileParser(testFileIO);
			string filePath = "test";

			// Act
			int expected = parser.DeserializeFromFile<int>(filePath);
			int actual = value;

			// Assert
			Assert.Equal(expected, actual);
		}
		#endregion

		#region "DeserializeArrayFromFile"
		[Fact]
		public void Test_DeserializeArrayFromFile_ReturnDeserializedValues()
		{
			// Arrange
			int value = 1;
			TestArrayFileIO<int> testFileIO = new([value]);
			IFileParser parser = CreateFileParser(testFileIO);
			string filePath = "test";

			// Act
			ICollection<int> expected = parser.DeserializeArrayFromFile<int>(filePath);
			ICollection<int> actual = [value];

			// Assert
			Assert.Equal(expected, actual);
		}
		#endregion
	}
}
