namespace LogWrapper.Loggers.Null
{
	internal class NullLogger(Type pDeclaringType) : BaseLogger(pDeclaringType), ILogger
	{
		#region "ILogger"
		public void Debug(object pMessage)
		{
			// Do Nothing
		}

		public void Error(object pMessage)
		{
			// Do Nothing
		}

		public void Fatal(object pMessage)
		{
			// Do Nothing
		}

		public void Info(object pMessage)
		{
			// Do Nothing
		}

		public void Warn(object pMessage)
		{
			// Do Nothing
		}

		#endregion "ILogger"
	}
}
