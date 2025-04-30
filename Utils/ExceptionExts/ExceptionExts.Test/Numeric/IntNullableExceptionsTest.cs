using FluentAssertions;

namespace ExceptionExts.Test.Numeric
{
    public class IntNullableExceptionsTest
    {
        #region Negative
        [Fact]
        public void NullOrNegative_IsNull_ThrowArgException()
        {
            int? val = null;

            string expMsg = $"{nameof(val)} is null or negative.";
            Action act = void () => val.ThrowIf().NullOrNegative();

            act.Should().Throw<ArgumentException>().WithMessage(expMsg);
        }

        [Fact]
        public void NullOrNegative_IsNegative_ThrowArgException()
        {
            int? val = -1;

            string expMsg = $"{nameof(val)} is null or negative.";
            Action act = void () => val.ThrowIf().NullOrNegative();

            act.Should().Throw<ArgumentException>().WithMessage(expMsg);
        }

        [Fact]
        public void NullOrNegative_IsNotNegative_DontThrowArgException()
        {
            int? val = 1;

            Action act = void () => val.ThrowIf().NullOrNegative();

            act.Should().NotThrow<ArgumentException>();
        }
        #endregion
    }
}
