namespace ExceptionExts.Extensions.String
{
    internal class StringExceptions(string pArgName, string? pVal) : BaseArgExceptions(pArgName), IStringExceptions
    {
        private readonly string? _val = pVal;

        #region IStringExceptions
        public void NullOrWhiteSpace()
        {
            if (string.IsNullOrWhiteSpace(_val))
            {
                throw new InvalidOperationException($"{_argName} is null or blank.");
            }
        }
        #endregion
    }
}
