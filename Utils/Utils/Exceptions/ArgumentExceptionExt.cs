namespace Utils.Exceptions
{
	public static class ArgumentExceptionExt
	{
		public static void ThrowIfNullOrEmpty<TItem>(ICollection<TItem>? pCollection)
		{
			if (pCollection == null || pCollection.Count == 0)
			{
				throw new ArgumentException(nameof(pCollection));
			}
		}
	}
}
