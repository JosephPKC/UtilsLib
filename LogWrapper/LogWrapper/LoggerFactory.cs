using LogWrapper.ColorConsole;

namespace LogWrapper
{
    /// <summary>
    /// Constructs the Logger for the client.
    /// </summary>
    public static class LoggerFactory
	{
		public static ILogger CreateNewLogger(Type pDeclaringType, LoggerConfigs? pConfigs = null)
		{
			if (pConfigs == null)
			{
				return CreateDefaultLogger(pDeclaringType);
			}

			if (pConfigs.UseColorCoding)
			{
				return new ColorConsoleLogger(pDeclaringType);
			}

			return CreateDefaultLogger(pDeclaringType);
		}

		private static ColorConsoleLogger CreateDefaultLogger(Type pDeclaringType)
		{
			return new ColorConsoleLogger(pDeclaringType);
		}
	}
}
