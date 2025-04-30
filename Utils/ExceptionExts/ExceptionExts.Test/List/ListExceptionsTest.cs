using FluentAssertions;

namespace ExceptionExts.Test.List
{
    public class ListExceptionsTest
    {
        #region NullOrEmpty
        [Fact]
        public void NullOrEmpty_IsNull_ThrowArgException()
        {
            List<int>? li = null;

            string expMsg = $"{nameof(li)} is null or empty.";
            Action act = void() => li.ThrowIf().NullOrEmpty();

            act.Should().Throw<ArgumentException>().WithMessage(expMsg);
        }

        [Fact]
        public void NullOrEmpty_IsEmpty_ThrowArgException()
        {
            List<int>? li = [];

            string expMsg = $"{nameof(li)} is null or empty.";
            Action act = void () => li.ThrowIf().NullOrEmpty();

            act.Should().Throw<ArgumentException>().WithMessage(expMsg);
        }

        [Fact]
        public void NullOrEmpty_IsNeitherNullNorEmpty_DontThrowArgException()
        {
            List<int>? li = [1];

            Action act = void () => li.ThrowIf().NullOrEmpty();

            act.Should().NotThrow<ArgumentException>();
        }
        #endregion

        #region OutOfRange
        [Fact]
        public void OutOfRange_IsNull_DontThrowArgOutOfRangeException()
        {
            List<int>? li = null;   //  Invalid for the exception
            int index = 1;

            Action act = void () => li.ThrowIf().OutOfRange(index);

            act.Should().NotThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void OutOfRange_IsOutOfRange_ThrowArgOutOfRangeException()
        {
            List<int>? li = [1];
            int index = 1;

            Action act = void () => li.ThrowIf().OutOfRange(index);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void OutOfRange_WithinRange_DontThrowArgOutOfRangeException()
        {
            List<int>? li = [1];
            int index = 0;

            Action act = void () => li.ThrowIf().OutOfRange(index);

            act.Should().NotThrow<ArgumentOutOfRangeException>();
        }
        #endregion
    }
}
