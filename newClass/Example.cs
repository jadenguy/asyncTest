using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncExample
{
    public class Example
    {
        int threadsRun;
        int threadsRequested;
        private Stopwatch runTimer;

        public int ThreadsRun
        {
            get => threadsRun; private set
            {
                threadsRun = value;
                System.Console.WriteLine($"{ThreadsRun} Thread Done");
            }
        }
        public int ThreadsRequested { get => threadsRequested; private set => threadsRequested = value; }
        public Stopwatch RunTimer { get => runTimer; private set => runTimer = value; }
        public bool Running { get { return ((ThreadsRequested - ThreadsRun) != 0); } }
        public Example()
        {
            RunTimer = new Stopwatch();
        }
        public async Task<double> StartSleep(double sleepSeconds)
        {
            ThreadsRequested += 1;
            var sleepMilliseconds = (int)(sleepSeconds * 1000);
            await Task.Delay(sleepMilliseconds);
            ThreadsRun += 1;
            return sleepSeconds;
        }
        public double StartSleepSequential(double sleepSeconds, int threads)
        {
            var ret = 0d;
            for (int i = 1; i <= threads; i++)
            {
                ret += StartSleep(sleepSeconds).GetAwaiter().GetResult();
            }
            return ret;
        }
        public async Task<double> ThreadSleepAsync(double sleepSeconds, int threads)
        {
            var threadResultArray = new Task<double>[threads];
            for (int i = 0; i < threads; i++)
            {
                threadResultArray[i] = Task.Run(() => StartSleep(sleepSeconds));
            }
            var totalResults = 0d;
            foreach (var total in threadResultArray)
            {
                totalResults += await total;
            }
            return totalResults;
        }
        public override string ToString() => string.Format("REQ {0}, RUN {1}", threadsRequested, threadsRun);
    }
}