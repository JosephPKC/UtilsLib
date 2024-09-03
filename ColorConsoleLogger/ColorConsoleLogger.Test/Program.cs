namespace ColorConsoleLogger.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ColorConsole log = new(typeof(Program));

            log.Debug("This is a DEBUG message!");
            log.Error("This is an ERROR message!");
            log.Fatal("This is a FATAL message!");
            log.Info("This is an INFO message!");
            log.Warn("This is a WARN message!");
        }
    }
}