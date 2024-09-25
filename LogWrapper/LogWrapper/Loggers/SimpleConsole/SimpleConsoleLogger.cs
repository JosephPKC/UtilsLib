
namespace LogWrapper.Loggers.SimpleConsole
{
    internal class SimpleConsoleLogger(Type pDeclaringType) : BaseLogger(pDeclaringType), ILogger
    {
		#region "ILogger"
		public void Debug(object pMessage)
        {
            Console.WriteLine($"DEBUG ({_declaringType}): {pMessage}");
        }

        public void Error(object pMessage)
        {
			Console.WriteLine($"ERROR ({_declaringType}): {pMessage}");
        }

        public void Fatal(object pMessage)
        {
			Console.WriteLine($"FATAL ({_declaringType}): {pMessage}");
        }

        public void Info(object pMessage)
        {
			Console.WriteLine($"INFO ({_declaringType}): {pMessage}");
        }

        public void Warn(object pMessage)
        {
			Console.WriteLine($"WARN ({_declaringType}): {pMessage}");
        }
        #endregion
    }
}
