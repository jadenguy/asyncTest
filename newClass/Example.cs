using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncExample
{
    public class Example
    {
        int threadsRun;
        int threadsRequested;
        public int ThreadsRun { get => threadsRun; private set => threadsRun = value; }
        public int ThreadsRequested { get => threadsRequested; private set => threadsRequested = value; }
        public bool Running { get { return ((ThreadsRequested - ThreadsRun) > 0); } }
        public int StartSleep(int sleepSeconds)
        {
            ThreadsRequested += sleepSeconds;
            Thread.Sleep(sleepSeconds * 1000);
            ThreadsRun += 1;
            return sleepSeconds;
        }
        public int StartSleepSequential(int sleepSeconds, int threads)
        {
            var ret = 0;
            for (int i = 1; i <= threads; i++)
            {
                ret += StartSleep(sleepSeconds);
            }
            return ret;
        }
        public async Task<int> StartSleepAsync(int sleepSeconds)
        {
            return await Task.Run(() => StartSleep(sleepSeconds));
        }

        public async Task<int> ThreadSleepAsync(int sleepSeconds, int threads)
        {
            var threadResultArray = new Task<int>[threads];
            for (int i = 0; i < threads; i++)
            {
                threadResultArray[i] = Task.Run(() => StartSleep(sleepSeconds));
            }
            var totalResults = 0;

            foreach (var total in threadResultArray)
            {
                totalResults += await total;
            }
            return totalResults;
        }

        public async void BackgroundThreadSleepAsync(int sleepSeconds, int threads)
        {
            var x = 0;
            for (int i = 0; i < threads; i++)
            {
                x += await Task.Run(() => StartSleep(sleepSeconds));
            }
        }
    }
}