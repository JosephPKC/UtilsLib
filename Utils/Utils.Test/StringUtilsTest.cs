namespace Utils.Test
{
	public class StringUtilsTest
	{
		[Theory]
		[InlineData("old", "new", true, "old new")]
		[InlineData("old", "new", false, "new")]
		[InlineData("old", "", true, "old")]
		[InlineData("old", "",  false, "")]
		[InlineData("old", null, true, "old")]
		[InlineData("old", null, false, "")]
		[InlineData("", "new", true, " new")]
		[InlineData("", "new", false, "new")]
		[InlineData("", null, true, "")]
		[InlineData("", null, false, "")]
		public void Test_SetOrAppend_ReturnAppendedString(string pOldStr, string? pNewStr, bool pIsAppend, string pExpectedStr)
		{
			// Arrange
			string oldStr = pOldStr;
			string? newStr = pNewStr;
			string delim = " ";
			bool isAppend = pIsAppend;

			// Act
			string actual = StringUtils.SetOrAppend(oldStr, newStr, delim, isAppend);
			string expected = pExpectedStr;

			// Assert
			Assert.Equal(expected, actual);
		}
	}
}