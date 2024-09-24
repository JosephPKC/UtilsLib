using Utils.Exceptions;

namespace Utils.Test.Exceptions
{
	public class ArgumentOutOfRangeExceptionExtTest
	{
		[Fact]
		public void Test_ThrowIfOutOfRange_EmptyCollection_ThrowsArgumentOutOfRangeException()
		{
			// Arrange
			IList<string> col = [];
			int index = 0;

			// Act
			void action() => ArgumentOutOfRangeExceptionExt.ThrowIfOutOfRange(col, index);

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(action);
		}

		[Fact]
		public void Test_ThrowIfOutOfRange_IndexBeyondCollectionSize_ThrowsArgumentOutOfRangeException()
		{
			// Arrange
			IList<string> col = ["test"];
			int index = 2;

			// Act
			void action() => ArgumentOutOfRangeExceptionExt.ThrowIfOutOfRange(col, index);

			// Assert
			Assert.Throws<ArgumentOutOfRangeException>(action);
		}

		[Fact]
		public void Test_ThrowIfOutOfRange_IndexWithinCollectionSize_DontThrowArgumentOutOfRangeException()
		{
			// Arrange
			IList<string> col = ["test"];
			int index = 0;

			// Act
			bool hasException = false;
			try
			{
				ArgumentOutOfRangeExceptionExt.ThrowIfOutOfRange(col, index);
			}
			catch (ArgumentOutOfRangeException)
			{
				hasException = true;
			}
			// Assert
			Assert.False(hasException);
		}
	}
}
