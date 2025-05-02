using LogWrapper.Loggers;

namespace LogWrapper.Test
{
    public static class Program
	{
		/* Console Tests */
		public static void Main(string[] args)
		{
			Test("ColorConsoleLogger", TestColorConsole);
			Test("NullLogger", TestNull);
			Test("SimpleConsoleLogger", TestSimpleConsole);
		}

		private static void Test(string pTestName, Action pTest)
		{
			Console.WriteLine($"*** BEGIN TESTING {pTestName} ***");
			pTest();
			Console.WriteLine($"*** END TESTING {pTestName} ***");
		}

		private static void TestColorConsole()
		{
			ILogger log = LoggerFacFactory.CreateColorConsoleLoggerFactory().CreateNewLogger(typeof(Program));
			log.LogLevel = LogLevels.Debug;

			log.Debug("THIS IS A DEBUG MESSAGE.");
			log.Error("THIS IS AN ERROR MESSAGE.");
			log.Fatal("THIS IS A FATAL MESSAGE.");
			log.Info("THIS IS AN INFO MESSAGE.");
			log.Warn("THIS IS A WARN MESSAGE.");
		}

		private static void TestNull()
		{
			ILogger log = LoggerFacFactory.CreateNullLoggerFactory().CreateNewLogger(typeof(Program));
            log.LogLevel = LogLevels.Debug;

            log.Debug("THIS IS A DEBUG MESSAGE.");
			log.Error("THIS IS AN ERROR MESSAGE.");
			log.Fatal("THIS IS A FATAL MESSAGE.");
			log.Info("THIS IS AN INFO MESSAGE.");
			log.Warn("THIS IS A WARN MESSAGE.");
		}

		private static void TestSimpleConsole()
		{
			ILogger log = LoggerFacFactory.CreateSimpleConsoleLoggerFactory().CreateNewLogger(typeof(Program));
            log.LogLevel = LogLevels.Debug;

            log.Debug("THIS IS A DEBUG MESSAGE.");
			log.Error("THIS IS AN ERROR MESSAGE.");
			log.Fatal("THIS IS A FATAL MESSAGE.");
			log.Info("THIS IS AN INFO MESSAGE.");
			log.Warn("THIS IS A WARN MESSAGE.");
		}
	}
}
