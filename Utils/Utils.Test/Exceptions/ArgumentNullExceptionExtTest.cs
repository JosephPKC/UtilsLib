using Utils.Exceptions;

namespace Utils.Test.Exceptions
{
	public class ArgumentNullExceptionExtTest
	{
		[Fact]
		public void Test_ThrowIfNullOrEmpty_NullCollection_ThrowsArgumentNullException()
		{
			// Arrange
			ICollection<string>? col = null;

			// Act
			void action() => ArgumentNullExceptionExt.ThrowIfNullOrEmpty(col);

			// Assert
			Assert.Throws<ArgumentNullException>(action);
		}

		[Fact]
		public void Test_ThrowIfNullOrEmpty_EmptyCollection_ThrowsArgumentNullException()
		{
			// Arrange
			ICollection<string>? col = [];

			// Act
			void action() => ArgumentNullExceptionExt.ThrowIfNullOrEmpty(col);

			// Assert
			Assert.Throws<ArgumentNullException>(action);
		}

		[Fact]
		public void Test_ThrowIfNullOrEmpty_NonEmptyCollection_DontThrowArgumentNullException()
		{
			// Arrange
			ICollection<string>? col = ["test"];

			// Act
			bool hasException = false;
			try
			{
				ArgumentNullExceptionExt.ThrowIfNullOrEmpty(col);
			}
			catch (ArgumentNullException)
			{
				hasException = true;
			}
			// Assert
			Assert.False(hasException);
		}
	}
}
