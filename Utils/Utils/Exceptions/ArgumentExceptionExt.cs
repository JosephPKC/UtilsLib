namespace Utils.Exceptions
{
	public static class ArgumentExceptionExt
	{
		public static void ThrowIfNullOrEmpty<TItem>(string pArgName, ICollection<TItem>? pCollection)
		{
			if (pCollection == null || pCollection.Count == 0)
			{
				throw new ArgumentException($"{pArgName} is null or empty.");
			}
		}

		public static void ThrowIfNegative(string pArgName, int pArg)
		{
            if (pArg < 0)
            {
				throw new ArgumentException($"{pArgName} is negative.");
            }
        }
	}
}
