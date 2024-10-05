using Moq;

using FileIOWrapper;

using JsonParser.Parsers;
using JsonParser.Parsers.TextJsonStringParser;

using JsonParser.Test.Fakes;

namespace JsonParser.Test.Parsers
{
	/// <summary>
	/// Tests TextJsonFileParser.
	/// </summary>
	public class TextJsonStringParserTest
	{
		public static IStringParser CreateStringParser()
		{
			return new TextJsonStringParserFactory().CreateNewParser();
		}

		#region "Serialize"
		[Fact]
		public void Test_Serialize_ReturnSerializedValue()
		{
			// Arrange
			IStringParser parser = CreateStringParser();
			int value = 1;

			// Act
			string actual = parser.Serialize(value);
			string expected = "1";

			// Assert
			Assert.Equal(expected, actual);
		}
		#endregion

		#region "Deserialize"
		[Fact]
		public void Test_Deserialize_ReturnDeserializedValue()
		{
			// Arrange
			IStringParser parser = CreateStringParser();
			string value = "1";

			// Act
			int actual = parser.Deserialize<int>(value);
			int expected = 1;

			// Assert
			Assert.Equal(expected, actual);
		}
		#endregion

		#region "DeserializeArray"
		[Fact]
		public void Test_DeserializeArray_ReturnDeserializedValues()
		{
			// Arrange
			IStringParser parser = CreateStringParser();
			string value = "[1]";

			// Act
			ICollection<int> actual = parser.DeserializeArray<int>(value);
			ICollection<int> expected = [1];

			// Assert
			Assert.Equal(expected, actual);
		}
		#endregion
	}
}
