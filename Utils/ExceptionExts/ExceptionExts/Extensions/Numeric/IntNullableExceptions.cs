namespace ExceptionExts.Extensions.Numeric
{
    internal class IntNullableExceptions(string pArgName, int? pNumeric) : BaseArgExceptions(pArgName), INullableNumericExceptions
    {
        private readonly int? _numeric = pNumeric;

        #region INullableNumericExceptions
        public void NullOrNegative()
        {
            if (_numeric is null || _numeric < 0)
            {
                throw new ArgumentException($"{_argName} is null or negative.");
            }
        }
        #endregion
    }
}
