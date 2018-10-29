using System;

namespace AsyncExample
{
    class consoleApp
    {
        static void Main()
        {
            LogThis("Starting");
            var n = new AsyncExample.Sleeper();
            n.ThreadSleepAsync(10,10).Wait();
            LogThis("Done");
        }
        private static void LogThis(string message)
        {
            var nowtime = DateTime.Now.ToString("o");
            System.Console.WriteLine($"{nowtime}: {message}");
        }
    }
}