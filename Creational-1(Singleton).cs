using System;

namespace SingletonPatternDemo
{
    public sealed class Logger
    {
        private static readonly Logger instance = new Logger();
        private Logger() { }
        public static Logger Instance => instance;

        public void Log(string msg) => Console.WriteLine($"LOG: {msg}");
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("\n--- Singleton Pattern Example ---");
            var logger1 = Logger.Instance;
            var logger2 = Logger.Instance;

            logger1.Log("First message");
            logger2.Log("Second message");

            Console.WriteLine($"logger1 is logger2: {ReferenceEquals(logger1, logger2)}");
        }
    }
}
