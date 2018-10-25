using System.Diagnostics;
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
            var expectedTotal = sleepSeconds;
            //-- Act
            stopwatch.Start();
            example.StartSleep(sleepSeconds);
            stopwatch.Stop();
            var actualSeconds = stopwatch.Elapsed.Seconds;
            var actualTotal = example.ThreadsRun;
            //-- Assert
            Assert.AreEqual(expectedSeconds, actualSeconds);
            Assert.AreEqual(expectedTotal, actualTotal);
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
            var expectedTotal = sleepSeconds * threads;

            //-- Act
            var actualReturnTotal = 0;
            stopwatch.Start();
            actualReturnTotal += example.StartSleepSequential(sleepSeconds, threads);
            stopwatch.Stop();
            var actualSeconds = stopwatch.Elapsed.Seconds;
            var actualTotal = example.ThreadsRun;

            //-- Assert
            Assert.AreEqual(expectedSeconds, actualSeconds);
            Assert.AreEqual(expectedTotal, actualReturnTotal);
            Assert.AreEqual(expectedTotal, actualTotal);
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
            var expectedTotal = sleepSeconds * threads;

            //-- Act

            stopwatch.Start();
            int actualReturnTotal = await example.ThreadSleepAsync(sleepSeconds, threads);
            stopwatch.Stop();
            var actualSeconds = stopwatch.Elapsed.Seconds;
            var actualTotal = example.ThreadsRun;

            //-- Assert
            Assert.AreEqual(expectedSeconds, actualSeconds);
            Assert.AreEqual(expectedTotal, actualReturnTotal);
            Assert.AreEqual(expectedTotal, actualTotal);
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
            var expectedTotal = 0;

            //-- Act
            stopwatch.Start();
            example.BackgroundThreadSleepAsync(sleepSeconds, threads);
            stopwatch.Stop();
            var actualSeconds = stopwatch.Elapsed.Seconds;
            var actualTotal = example.ThreadsRun;

            //-- Assert
            Assert.AreEqual(expectedSeconds, actualSeconds);
            Assert.AreEqual(expectedTotal, actualTotal);
        }
    }
}