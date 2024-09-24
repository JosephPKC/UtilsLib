namespace Utils.Exceptions
{
	public static class ArgumentNullExceptionExt
	{
		public static void ThrowIfNullOrEmpty<TItem>(ICollection<TItem>? pCollection)
		{
			if (pCollection == null || pCollection.Count == 0)
			{
				throw new ArgumentNullException(nameof(pCollection));
			}
		}
	}
}
