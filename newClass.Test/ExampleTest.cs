using NUnit.Framework;

namespace AsyncExample.Tests
{
    public class ExampleTest
    {
        [TestCase(0)]
        [TestCase(5)]
        public void StartSleepTest(double sleepSeconds)
        {
            //-- Arrange
            var example = new Example();

            var expectedMilliseconds = sleepSeconds * 1000;
            var expectedRequest = 1;
            var expectedRun = 1;
            var expectedTotal = sleepSeconds;

            //-- Act
            example.RunTimer.Start();
            var actualTotal = example.StartSleep(sleepSeconds).Result;
            example.RunTimer.Stop();

            var actualMilliseconds = example.RunTimer.Elapsed.TotalMilliseconds;
            var actualRequest = example.ThreadsRequested;
            var actualRun = example.ThreadsRun;

            //-- Assert
            Assert.AreEqual(expectedMilliseconds, actualMilliseconds, 100);
            Assert.AreEqual(expectedTotal, actualTotal);
            Assert.AreEqual(expectedRun, actualRun);
            Assert.AreEqual(expectedRequest, actualRequest);
        }
        [TestCase(5, 1)]
        [TestCase(1, 5)]
        public void StartSleepSequentialTest(double sleepSeconds, int threads)
        {
            //-- Arrange
            var example = new Example();

            var expectedMilliseconds = sleepSeconds * threads * 1000;
            var expectedRun = threads;
            var expectedRequest = threads;
            var expectedTotal = sleepSeconds * threads;

            //-- Act
            example.RunTimer.Start();
            var actualTotal = example.StartSleepSequential(sleepSeconds, threads);
            example.RunTimer.Stop();

            var actualMilliseconds = example.RunTimer.Elapsed.TotalMilliseconds;
            var actualRun = example.ThreadsRun;
            var actualRequest = example.ThreadsRequested;

            //-- Assert
            Assert.AreEqual(expectedMilliseconds, actualMilliseconds, 100);
            Assert.AreEqual(expectedTotal, actualTotal);
            Assert.AreEqual(expectedRun, actualRun);
            Assert.AreEqual(expectedRequest, actualRequest);
        }
        [TestCase(5, 10)]
        [TestCase(20, 5)]
        public void ThreadSleepAsyncTestAsync(double sleepSeconds, int threads)
        {
            //-- Arrange
            var example = new Example();

            var expectedMilliseconds = sleepSeconds * 1000;
            var expectedRequest = threads;
            var expectedRun = threads;
            var expectedTotal = sleepSeconds * threads;

            //-- Act
            example.RunTimer.Start();
            var actualTotal = example.ThreadSleepAsync(sleepSeconds, threads).GetAwaiter().GetResult();
            example.RunTimer.Stop();

            var actualMilliseconds = example.RunTimer.Elapsed.TotalMilliseconds;
            var actualRun = example.ThreadsRun;
            var actualRequest = example.ThreadsRequested;

            //-- Assert
            System.Console.WriteLine(example.ToString());
            Assert.AreEqual(expectedMilliseconds, actualMilliseconds, 100);
            Assert.AreEqual(expectedTotal, actualTotal);
            //-- Maybe I don't care if it knows it's internal state?
            // double delta = 1 + threads / 2d;
            // Assert.AreEqual(expectedRun, actualRun, delta);
            // Assert.AreEqual(expectedRequest, actualRequest, delta);
        }
        [TestCase(5, 1)]
        [TestCase(1, 5)]
        public void BackgroundThreadSleepAsyncTestAsync(double sleepSeconds, int threads)
        {
            //-- Arrange
            var example = new Example();
            var expectedMilliseconds = 0;
            var expectedRun = 0;
            var expectedRequest = 0;

            //-- Act
            example.RunTimer.Start();
            example.ThreadSleepAsync(sleepSeconds, threads);
            example.RunTimer.Stop();

            var actualMilliseconds = example.RunTimer.Elapsed.TotalMilliseconds;
            var actualRun = example.ThreadsRun;
            var actualRequest = example.ThreadsRequested;

            //-- Assert
            Assert.AreEqual(expectedMilliseconds, actualMilliseconds, 100);
            // double delta = 1 + threads / 2d;
            // Assert.AreEqual(expectedRequest, actualRequest, delta);
            // Assert.AreEqual(expectedRun, actualRun, 1);
        }
    }
}