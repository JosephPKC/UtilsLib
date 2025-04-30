using FluentAssertions;

namespace ExceptionExts.Test.Numeric
{
    public class IntExceptionsTest
    {
        #region Negative
        [Fact]
        public void Negative_IsNegative_ThrowArgException()
        {
            int val = -1;

            string expMsg = $"{nameof(val)} is negative.";
            Action act = void () => val.ThrowIf().Negative();

            act.Should().Throw<ArgumentException>().WithMessage(expMsg);
        }

        [Fact]
        public void Negative_IsNotNegative_DontThrowArgException()
        {
            int val = 1;

            Action act = void () => val.ThrowIf().Negative();

            act.Should().NotThrow<ArgumentException>();
        }
        #endregion
    }
}
