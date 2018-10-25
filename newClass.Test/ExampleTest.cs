using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace AsyncExample.Tests
{
    public class ExampleTest
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        public void StartSleepTest(int sleepSeconds)
        {
            //-- Arrange
            var example = new Example();
            var stopwatch = new Stopwatch();
            var expectedSeconds = sleepSeconds;
            var expectedRun = 1;
            var expectedTotal = sleepSeconds;
            //-- Act
            stopwatch.Start();
            var actualTotal = example.StartSleep(sleepSeconds);
            stopwatch.Stop();
            var actualSeconds = stopwatch.Elapsed.Seconds;
            var actualRun = example.ThreadsRun;
            //-- Assert
            Assert.AreEqual(expectedSeconds, actualSeconds);
            Assert.AreEqual(expectedRun, actualRun);
            Assert.AreEqual(expectedTotal, actualTotal);

        }
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 2)]
        public void StartSleepSequentialTest(int sleepSeconds, int threads)
        {
            //-- Arrange
            var example = new Example();
            var stopwatch = new Stopwatch();
            var expectedSeconds = sleepSeconds * threads;
            var expectedRun = threads;
            var expectedTotal = sleepSeconds * threads;

            //-- Act
            var actualTotal = 0;
            stopwatch.Start();
            actualTotal += example.StartSleepSequential(sleepSeconds, threads);
            stopwatch.Stop();
            var actualSeconds = stopwatch.Elapsed.Seconds;
            var actualRun = example.ThreadsRun;
            var actualRequested = example.ThreadsRequested;

            //-- Assert
            Assert.AreEqual(expectedSeconds, actualSeconds);
            Assert.AreEqual(expectedTotal, actualTotal);
            Assert.AreEqual(expectedRun, actualRun);
            Assert.AreEqual(expectedRun, actualRequested);
        }
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 2)]
        public async Task ThreadSleepAsyncTestAsync(int sleepSeconds, int threads)
        {
            //-- Arrange
            var example = new Example();
            var stopwatch = new Stopwatch();
            var expectedSeconds = sleepSeconds;
            var expectedRun = threads;
            var expectedTotal = sleepSeconds * threads;

            //-- Act

            stopwatch.Start();
            int actualTotal = await example.ThreadSleepAsync(sleepSeconds, threads);
            stopwatch.Stop();
            var actualSeconds = stopwatch.Elapsed.Seconds;
            var actualRun = example.ThreadsRun;
            var actualRequested = example.ThreadsRequested;


            //-- Assert
            Assert.AreEqual(expectedSeconds, actualSeconds);
            Assert.AreEqual(expectedTotal, actualTotal);
            Assert.AreEqual(expectedRun, actualRun);
            Assert.AreEqual(expectedRun, actualRequested);
        }
        [TestCase(1, 1)]
        [TestCase(1, 2)]
        [TestCase(2, 2)]
        public void BackgroundThreadSleepAsyncTestAsync(int sleepSeconds, int threads)
        {
            //-- Arrange
            var example = new Example();
            var stopwatch = new Stopwatch();
            var expectedSeconds = 0;
            var expectedRun = 0;

            //-- Act
            stopwatch.Start();
            example.BackgroundThreadSleepAsync(sleepSeconds, threads);
            stopwatch.Stop();
            var actualSeconds = stopwatch.Elapsed.Seconds;
            var actualRun = example.ThreadsRun;
            var actualRequested = example.ThreadsRequested;

            //-- Assert
            Assert.AreEqual(expectedSeconds, actualSeconds);
            Assert.AreEqual(expectedRun, actualRun);
            Assert.AreEqual(expectedRun, actualRequested);
        }
    
        public void BackgroundThreadSleepAsyncTestAwait(int sleepSeconds, int threads)
        {
            //-- Arrange
            var example = new Example();
            var stopwatch = new Stopwatch();
            var expectedSeconds = sleepSeconds;
            var expectedRun = threads;

            //-- Act
            stopwatch.Start();
            example.BackgroundThreadSleepAsync(sleepSeconds, threads);
            var i = 0;
            // do
            // {
            //     i++;
            // } while (example.Running||example.ThreadsRequested==0);
            System.Console.WriteLine(i);
            stopwatch.Stop();
            var actualSeconds = stopwatch.Elapsed.Seconds;
            var actualRun = example.ThreadsRun;
            var actualRequested = example.ThreadsRequested;

            //-- Assert
            Assert.AreEqual(expectedSeconds, actualSeconds);
            Assert.AreEqual(expectedRun, actualRun);
            Assert.AreEqual(expectedRun, actualRequested);
        }
    }
}