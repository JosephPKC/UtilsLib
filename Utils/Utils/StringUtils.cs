namespace Utils
{
	public static class StringUtils
	{
		public static string SetOrAppend(string pString, string? pNewString, string pDelim, bool pIsAppend)
		{
			if (string.IsNullOrWhiteSpace(pNewString))
			{
				return pIsAppend ? pString : "";
			}
			return pIsAppend ? $"{pString}{pDelim}{pNewString}" : pNewString;
		}
	}
}
