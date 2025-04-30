using FluentAssertions;
using Moq;

using FileIOWrapper;

using JsonParser.Parsers;

using JsonParser.Test.Fakes;

namespace JsonParser.Test.Parsers
{
	public class TextJsonFileParserTest
	{
        #region DeserializeFromFile
        [Fact]
        public void DeserializeFromFile_ReturnDeserializedValue()
        {
            int value = 1;
            TestFileIO<int> testFileIO = new(value);
            IFileParser parser = JsonParserFactory.CreateTextJsonFileParser(testFileIO);
            string filePath = "test";

            int expected = value;
            int actual = parser.DeserializeFromFile<int>(filePath);

            actual.Should().Be(expected);
        }
        #endregion

        #region DeserializeArrayFromFile
        [Fact]
        public void DeserializeArrayFromFile_ReturnDeserializedValues()
        {
            int value = 1;
            TestArrayFileIO<int> testFileIO = new([value]);
            IFileParser parser = JsonParserFactory.CreateTextJsonFileParser(testFileIO);
            string filePath = "test";

            IEnumerable<int> expected = [value];
            IEnumerable<int> actual = parser.DeserializeArrayFromFile<int>(filePath);

            actual.Should().BeEquivalentTo(expected);
        }
        #endregion

        #region SerializeToFile
        [Fact]
		public void SerializeToFile_WriteAllText()
		{
			Mock<IFileIO> mockFileIO = new();
			IFileParser parser = JsonParserFactory.CreateTextJsonFileParser(mockFileIO.Object);
			int value = 1;
			string filePath = "test";

			parser.SerializeToFile(value, filePath);

			mockFileIO.Verify(x => x.WriteAllText(It.IsAny<string>(), It.IsAny<string?>()), Times.Once());
		}
		#endregion
	}
}
