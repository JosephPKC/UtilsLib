using Utils.Exceptions;

namespace Utils.Test.Exceptions
{
	public class InvalidOperationExceptionExtTest
	{
		[Fact]
		public void Test_ThrowIfNullOrWhiteSpace_NullString_ThrowsInvalidOperationException()
		{
			// Arrange
			string? str = null;
			string name = "str";

			// Act
			void action() => InvalidOperationExceptionExt.ThrowIfNullOrWhiteSpace(name, str);

			// Assert
			Assert.Throws<InvalidOperationException>(action);
		}

		[Fact]
		public void Test_ThrowIfNullOrWhiteSpace_EmptyString_ThrowsInvalidOperationException()
		{
			// Arrange
			string? str = "";
			string name = "str";

			// Act
			void action() => InvalidOperationExceptionExt.ThrowIfNullOrWhiteSpace(name, str);

			// Assert
			Assert.Throws<InvalidOperationException>(action);
		}

		[Fact]
		public void Test_ThrowIfNullOrWhiteSpace_NonEmptyOrBlankString_DontThrowInvalidOperationException()
		{
			// Arrange
			string? str = "abc";
			string name = "str";

			// Act
			bool hasException = false;
			try
			{
				InvalidOperationExceptionExt.ThrowIfNullOrWhiteSpace(name, str);
			}
			catch (InvalidOperationException)
			{
				hasException = true;
			}
			// Assert
			Assert.False(hasException);
		}
	}
}
