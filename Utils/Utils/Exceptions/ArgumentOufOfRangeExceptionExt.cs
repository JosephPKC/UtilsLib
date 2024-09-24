namespace Utils.Exceptions
{
	public static class ArgumentOutOfRangeExceptionExt
	{
		public static void ThrowIfOutOfRange<TItem>(IList<TItem> pList, int pIndex)
		{
			if (pIndex >= pList.Count)
			{
				throw new ArgumentOutOfRangeException($"{pIndex} is out of range for {nameof(pList)}");
			}
		}
	}
}