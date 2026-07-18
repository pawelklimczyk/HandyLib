using NUnit.Framework;
using System.Threading;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLStopWatchTests
    {
        // Upper bounds are intentionally generous (not tight to sleep duration): these tests
        // verify the watcher measures elapsed time, not wall-clock precision. Tight margins
        // flake under CI/JIT/thread-scheduling jitter.
        private const long UpperBoundMs = 5000;

        [Test]
        public void ShouldUseDefaultKey()
        {
          var key=  HLStopWatch.Start();
            Thread.Sleep(500);
            long elapsed = HLStopWatch.Stop(key);

            Assert.Greater(elapsed, 0);
            Assert.Less(elapsed, UpperBoundMs);
        }

        [Test]
        public void ShouldUseSpecifiedKey()
        {
            string returnedKey = HLStopWatch.Start("key123");
            Thread.Sleep(500);
            long elapsed = HLStopWatch.Stop(returnedKey);

            Assert.AreEqual(returnedKey, "key123");
            Assert.Greater(elapsed, 0);
            Assert.Less(elapsed, UpperBoundMs);
        }

        [Test]
        public void ShouldRunTwoWatchersWithDifferentKeys()
        {
            string returnedKey1 = HLStopWatch.Start("key123");
            string returnedKey2 = HLStopWatch.Start("key456");
            Assert.AreNotEqual(returnedKey1, returnedKey2);

            Thread.Sleep(500);
            long elapsed1 = HLStopWatch.Stop(returnedKey1);

            Thread.Sleep(500);
            long elapsed2 = HLStopWatch.Stop(returnedKey2);

            Assert.Greater(elapsed1, 0);
            Assert.Less(elapsed1, UpperBoundMs);
            Assert.Greater(elapsed2, elapsed1);
            Assert.Less(elapsed2, UpperBoundMs * 2);
        }

        [Test]
        public void ShouldRunTwoWatchersWithNoKeys()
        {
            string returnedKey1 = HLStopWatch.Start();
            string returnedKey2 = HLStopWatch.Start();
            Assert.AreNotEqual(returnedKey1, returnedKey2);

            Thread.Sleep(500);
            long elapsed1 = HLStopWatch.Stop(returnedKey1);

            Thread.Sleep(500);
            long elapsed2 = HLStopWatch.Stop(returnedKey2);

            Assert.Greater(elapsed1, 0);
            Assert.Less(elapsed1, UpperBoundMs);
            Assert.Greater(elapsed2, elapsed1);
            Assert.Less(elapsed2, UpperBoundMs * 2);
        }
    }
}
