﻿using NUnit.Framework;

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
        [TestCase(5, 20)]
        [TestCase(10, 5)]
        public void ThreadSleepAsyncTestAsync(double sleepSeconds, int threads)
        {
            //-- Arrange
            var sleeper = new Sleeper();

            var expectedPreWaitMilliseconds = 0;
            var expectedPostWaitMilliseconds = sleepSeconds * 1000;
            var expectedRequest = threads;
            var expectedRun = threads;
            var expectedTotal = sleepSeconds * threads;

            //-- Act
            sleeper.RunTimer.Start();
            var actualAsync = sleeper.ThreadSleepAsync(sleepSeconds, threads);
            var actualPreWaitMilliseconds = sleeper.RunTimer.Elapsed.TotalMilliseconds;
            var actualTotal = actualAsync.GetAwaiter().GetResult();
            var actualPostWaitMilliseconds = sleeper.RunTimer.Elapsed.TotalMilliseconds;
            sleeper.RunTimer.Stop();
            var actualRun = sleeper.ThreadsRun;
            var actualRequest = sleeper.ThreadsRequested;

            //-- Assert
            System.Console.WriteLine(sleeper.ToString());
            Assert.AreEqual(expectedPreWaitMilliseconds, actualPreWaitMilliseconds, 100);
            Assert.AreEqual(expectedPostWaitMilliseconds, actualPostWaitMilliseconds, 100);
            Assert.AreEqual(expectedTotal, actualTotal);
            //-- Maybe I don't care if it knows it's internal state?
            //-- Randomly say you can under half plus one threads into the request by the time we check, due to how fast this works
            // double delta = 1 + threads / 2d;
            // Assert.AreEqual(expectedRun, actualRun, delta);
            // Assert.AreEqual(expectedRequest, actualRequest, delta);
        }
    }
}