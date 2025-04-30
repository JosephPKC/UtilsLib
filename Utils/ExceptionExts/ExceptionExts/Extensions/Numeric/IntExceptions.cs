namespace ExceptionExts.Extensions.Numeric
{
    internal class IntExceptions(string pArgName, int pNumeric) : BaseArgExceptions(pArgName), INumericExceptions<int>
    {
        private readonly int _numeric = pNumeric;

        #region IntExceptions
        public void Negative()
        {
            if (_numeric < 0)
            {
                throw new ArgumentException($"{_argName} is negative.");
            }
        }
        #endregion
    }
}
