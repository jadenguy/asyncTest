using System.Diagnostics;
using System.Threading.Tasks;
using newClass;
using NUnit.Framework;

namespace Tests
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
            var expected = sleepSeconds;
            var stopwatch = new Stopwatch();
            //-- Act
            stopwatch.Start();
            example.StartSleep(sleepSeconds);
            stopwatch.Stop();
            var actual = stopwatch.Elapsed.Seconds;
            //-- Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public async StartSleepAsync()
        {
            Assert.True;
        }
    }
}