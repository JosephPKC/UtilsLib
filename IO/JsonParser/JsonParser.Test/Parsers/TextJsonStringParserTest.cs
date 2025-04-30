using FluentAssertions;

using JsonParser.Parsers;

namespace JsonParser.Test.Parsers
{
	public class TextJsonStringParserTest
	{
        #region Deserialize
        [Fact]
        public void Deserialize_ReturnDeserializedValue()
        {
            IStringParser parser = JsonParserFactory.CreateTextJsonStringParser();
            string value = "1";

            int expected = 1;
            int actual = parser.Deserialize<int>(value);
            
            actual.Should().Be(expected);
        }
        #endregion

        #region DeserializeArray
        [Fact]
        public void DeserializeArray_ReturnDeserializedValues()
        {
            IStringParser parser = JsonParserFactory.CreateTextJsonStringParser();
            string value = "[1]";

            IEnumerable<int> expected = [1];
            IEnumerable<int> actual = parser.DeserializeArray<int>(value);
            
            actual.Should().BeEquivalentTo(expected);
        }
        #endregion

        #region Serialize
        [Fact]
		public void Serialize_ReturnSerializedValue()
		{
			IStringParser parser = JsonParserFactory.CreateTextJsonStringParser();
			int value = 1;

            string expected = "1";
            string actual = parser.Serialize(value);
			
            actual.Should().Be(expected);
		}
		#endregion
	}
}
