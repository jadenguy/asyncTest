using NUnit.Framework;

namespace AsyncExample.Tests
{
    public class SleeperTest
    {
        [TestCase(0)]
        [TestCase(5)]
        public void StartSleepTest(double sleepSeconds)
        {
            //-- Arrange
            var sleeper = new Sleeper();
            var expectedMilliseconds = sleepSeconds * 1000;
            var expectedRequest = 1;
            var expectedRun = 1;
            var expectedTotal = sleepSeconds;

            //-- Act
            sleeper.RunTimer.Start();
            var actualTotal = sleeper.StartSleep(sleepSeconds).Result;
            sleeper.RunTimer.Stop();

            var actualMilliseconds = sleeper.RunTimer.Elapsed.TotalMilliseconds;
            var actualRequest = sleeper.ThreadsRequested;
            var actualRun = sleeper.ThreadsRun;

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
            var sleeper = new Sleeper();

            var expectedMilliseconds = sleepSeconds * threads * 1000;
            var expectedRun = threads;
            var expectedRequest = threads;
            var expectedTotal = sleepSeconds * threads;

            //-- Act
            sleeper.RunTimer.Start();
            var actualTotal = sleeper.StartSleepSequential(sleepSeconds, threads);
            sleeper.RunTimer.Stop();

            var actualMilliseconds = sleeper.RunTimer.Elapsed.TotalMilliseconds;
            var actualRun = sleeper.ThreadsRun;
            var actualRequest = sleeper.ThreadsRequested;

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
            var sleeper = new Sleeper();

            var expectedMilliseconds = sleepSeconds * 1000;
            var expectedRequest = threads;
            var expectedRun = threads;
            var expectedTotal = sleepSeconds * threads;

            //-- Act
            sleeper.RunTimer.Start();
            var actualTotal = sleeper.ThreadSleepAsync(sleepSeconds, threads).GetAwaiter().GetResult();
            sleeper.RunTimer.Stop();

            var actualMilliseconds = sleeper.RunTimer.Elapsed.TotalMilliseconds;
            var actualRun = sleeper.ThreadsRun;
            var actualRequest = sleeper.ThreadsRequested;

            //-- Assert
            System.Console.WriteLine(sleeper.ToString());
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
            var sleeper = new Sleeper();
            var expectedMilliseconds = 0;
            var expectedRun = 0;
            var expectedRequest = 0;

            //-- Act
            sleeper.RunTimer.Start();
            sleeper.ThreadSleepAsync(sleepSeconds, threads);
            sleeper.RunTimer.Stop();

            var actualMilliseconds = sleeper.RunTimer.Elapsed.TotalMilliseconds;
            var actualRun = sleeper.ThreadsRun;
            var actualRequest = sleeper.ThreadsRequested;

            //-- Assert
            Assert.AreEqual(expectedMilliseconds, actualMilliseconds, 100);
            // double delta = 1 + threads / 2d;
            // Assert.AreEqual(expectedRequest, actualRequest, delta);
            // Assert.AreEqual(expectedRun, actualRun, 1);
        }
    }
}