using FluentAssertions;

namespace ExceptionExts.Test.String
{
    public class StringExceptionsTest
    {
        #region NullOrWhiteSpace
        [Fact]
        public void NullOrWhiteSpace_IsNull_ThrowArgException()
        {
            string? val = null;

            string expMsg = $"{nameof(val)} is null or blank.";
            Action act = void () => val.ThrowIf().NullOrWhiteSpace();

            act.Should().Throw<InvalidOperationException>().WithMessage(expMsg);
        }

        [Fact]
        public void NullOrWhiteSpace_IsWhiteSpace_ThrowArgException()
        {
            string? val = string.Empty;

            string expMsg = $"{nameof(val)} is null or blank.";
            Action act = void () => val.ThrowIf().NullOrWhiteSpace();

            act.Should().Throw<InvalidOperationException>().WithMessage(expMsg);
        }

        [Fact]
        public void NullOrWhiteSpace_IsNotNullNorWhiteSpace_DontThrowArgException()
        {
            string? val = "test";

            Action act = void () => val.ThrowIf().NullOrWhiteSpace();

            act.Should().NotThrow<InvalidOperationException>();
        }
        #endregion
    }
}
