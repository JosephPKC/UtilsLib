namespace LogWrapper.Loggers.Null
{
	internal class NullLogger(Type pDeclaringType) : BaseLogger(pDeclaringType)
	{
        #region BaseLogger
        public override void Debug(object? pMessage)
		{
			// Do Nothing
		}

		public override void Error(object? pMessage)
		{
			// Do Nothing
		}

		public override void Fatal(object? pMessage)
		{
			// Do Nothing
		}

		public override void Info(object? pMessage)
		{
			// Do Nothing
		}

		public override void Warn(object? pMessage)
		{
			// Do Nothing
		}
		#endregion
	}
}
