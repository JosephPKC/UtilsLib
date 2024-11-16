using Utils.Exceptions;

namespace Utils.Test.Exceptions
{
	public class ArgumentExceptionExtTest
	{
		[Fact]
		public void Test_ThrowIfNullOrEmpty_NullCollection_ThrowsArgumentException()
		{
			// Arrange
			ICollection<string>? col = null;

			// Act
			void action() => ArgumentExceptionExt.ThrowIfNullOrEmpty(nameof(col), col);

			// Assert
			Assert.Throws<ArgumentException>(action);
		}

		[Fact]
		public void Test_ThrowIfNullOrEmpty_EmptyCollection_ThrowsArgumentException()
		{
			// Arrange
			ICollection<string>? col = [];

			// Act
			void action() => ArgumentExceptionExt.ThrowIfNullOrEmpty(nameof(col), col);

			// Assert
			Assert.Throws<ArgumentException>(action);
		}

		[Fact]
		public void Test_ThrowIfNullOrEmpty_NonEmptyCollection_DontThrowArgumentException()
		{
			// Arrange
			ICollection<string>? col = ["test"];

			// Act
			bool hasException = false;
			try
			{
				ArgumentExceptionExt.ThrowIfNullOrEmpty(nameof(col), col);
			}
			catch (ArgumentException)
			{
				hasException = true;
			}
			// Assert
			Assert.False(hasException);
		}
	}
}
