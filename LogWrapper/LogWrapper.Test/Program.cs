using LogWrapper.Loggers;
using LogWrapper.Loggers.Log4Net.ColorConsole;
using LogWrapper.Loggers.Null;
using LogWrapper.Loggers.SimpleConsole;

namespace LogWrapper.Test
{
    public class Program
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
			ILogger log = new ColorConsoleLoggerFactory().CreateNewLogger(typeof(Program));

			log.Debug("THIS IS A DEBUG MESSAGE.");
			log.Error("THIS IS AN ERROR MESSAGE.");
			log.Fatal("THIS IS A FATAL MESSAGE.");
			log.Info("THIS IS AN INFO MESSAGE.");
			log.Warn("THIS IS A WARN MESSAGE.");
		}

		private static void TestNull()
		{
			ILogger log = new NullLoggerFactory().CreateNewLogger(typeof(Program));

			log.Debug("THIS IS A DEBUG MESSAGE.");
			log.Error("THIS IS AN ERROR MESSAGE.");
			log.Fatal("THIS IS A FATAL MESSAGE.");
			log.Info("THIS IS AN INFO MESSAGE.");
			log.Warn("THIS IS A WARN MESSAGE.");
		}

		private static void TestSimpleConsole()
		{
			ILogger log = new SimpleConsoleLoggerFactory().CreateNewLogger(typeof(Program));

			log.Debug("THIS IS A DEBUG MESSAGE.");
			log.Error("THIS IS AN ERROR MESSAGE.");
			log.Fatal("THIS IS A FATAL MESSAGE.");
			log.Info("THIS IS AN INFO MESSAGE.");
			log.Warn("THIS IS A WARN MESSAGE.");
		}
	}
}
