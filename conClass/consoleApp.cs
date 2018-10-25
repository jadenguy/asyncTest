using System;

namespace AsyncExample
{
    class consoleApp
    {
        static void Main()
        {
            System.Console.WriteLine("Starting");
            var example = new Example();
            example.BackgroundThreadSleepAsync(1,10);
            var example2 = new Example();
            example2.StartSleep(30);
            System.Console.WriteLine($"Background {example.ThreadsRun}");
            System.Console.WriteLine($"Sleep {example2.ThreadsRun}");
            System.Console.WriteLine("Done");
        }
    }
}