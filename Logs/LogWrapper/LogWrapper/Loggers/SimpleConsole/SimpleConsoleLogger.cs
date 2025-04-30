namespace LogWrapper.Loggers.SimpleConsole
{
    internal class SimpleConsoleLogger(Type pDeclaringType) : BaseLogger(pDeclaringType)
    {
        #region BaseLogger
        public override void Debug(object? pMessage)
        {
            WriteLog(LogLevels.Debug, pMessage, WriteToConsole);
        }

        public override void Error(object? pMessage)
        {
            WriteLog(LogLevels.Error, pMessage, WriteToConsole);
        }

        public override void Fatal(object? pMessage)
        {
            WriteLog(LogLevels.Fatal, pMessage, WriteToConsole);
        }

        public override void Info(object? pMessage)
        {
            WriteLog(LogLevels.Info, pMessage, WriteToConsole);
        }

        public override void Warn(object? pMessage)
        {
            WriteLog(LogLevels.Warn, pMessage, WriteToConsole);
        }
        #endregion

        private void WriteToConsole(LogLevels pLevel, object? pMessage)
        {
            Console.WriteLine($"{pLevel.ToString().ToUpper()} ({_declaringType}): {pMessage}");
        }
    }
}
