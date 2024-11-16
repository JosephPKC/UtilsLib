namespace Utils.Exceptions
{
	public static class InvalidOperationExceptionExt
	{
		public static void ThrowIfNullOrWhiteSpace(string pName, string? pString)
		{
			if (string.IsNullOrWhiteSpace(pString))
			{
				throw new InvalidOperationException($"{pName} is null or blank.");
			}
		}
	}
}