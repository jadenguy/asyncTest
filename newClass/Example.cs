using System;
using System.Threading;
using System.Threading.Tasks;

namespace newClass
{
    public class Example
    {
        public int StartSleep(int sleepSeconds)
        {
            Thread.Sleep(sleepSeconds * 1000);
            System.Console.WriteLine($"Sleep Done");
            return 0;
        }
    }
}
