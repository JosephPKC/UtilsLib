namespace LogWrapper.Test
{
	public class Program
	{
		/* Console Tests */
		public static void Main(string[] args)
		{
			Test("ColorConsoleLogger", TestColorConsole);
		}

		private static void Test(string pTestName, Action pTest)
		{
			Console.WriteLine($"*** BEGIN TESTING {pTestName} ***");
			pTest();
			Console.WriteLine($"*** END TESTING {pTestName} ***");
		}

		private static void TestColorConsole()
		{
			// At the moment, only Color Console is supported.
			ILogger log = LoggerFactory.CreateNewLogger(typeof(Program));

			log.Debug("THIS IS A DEBUG MESSAGE.");
			log.Error("THIS IS AN ERROR MESSAGE.");
			log.Fatal("THIS IS A FATAL MESSAGE.");
			log.Info("THIS IS AN INFO MESSAGE.");
			log.Warn("THIS IS A WARN MESSAGE.");
		}
	}
}
