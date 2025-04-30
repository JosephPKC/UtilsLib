namespace ExceptionExts.Extensions.List
{
    internal class ListExceptions<TValue>(string pArgName, ICollection<TValue>? pCol) : BaseArgExceptions(pArgName), IListExceptions<TValue>
    {
        private readonly ICollection<TValue>? _col = pCol;

        #region IListExceptions<TValue>
        public void NullOrEmpty()
        {
            if (_col is null || _col.Count == 0)
            {
                throw new ArgumentException($"{_argName} is null or empty.");
            }
        }

        public void OutOfRange(int pIndex)
        {
            if (_col is null)
            {
                return;
            }

            if (pIndex >= _col.Count)
            {
                throw new ArgumentOutOfRangeException(_argName);
            }
        }
        #endregion
    }
}
