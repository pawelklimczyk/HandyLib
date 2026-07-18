using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLTasksTests
    {
        [Test]
        public async Task PrimitiveSleep_WithoutAnyToken_ShouldCompleteAfterConfiguredDuration()
        {
            var stopwatch = Stopwatch.StartNew();

            // Note: implementation uses TimeSpan.FromSeconds despite the "minutes" parameter name.
            await HLTasks.PrimitiveSleep(1);

            stopwatch.Stop();
            Assert.GreaterOrEqual(stopwatch.ElapsedMilliseconds, 900);
        }

        [Test]
        public async Task PrimitiveSleep_WithUncancelledToken_ShouldCompleteAfterConfiguredDuration()
        {
            var stopwatch = Stopwatch.StartNew();

            // Note: implementation uses TimeSpan.FromSeconds despite the "minutes" parameter name.
            await HLTasks.PrimitiveSleep(1, CancellationToken.None);

            stopwatch.Stop();
            Assert.GreaterOrEqual(stopwatch.ElapsedMilliseconds, 900);
        }

        [Test]
        public async Task PrimitiveSleep_WithCancelledToken_ShouldReturnWithoutThrowing()
        {
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            var stopwatch = Stopwatch.StartNew();
            await HLTasks.PrimitiveSleep(30, cts.Token);
            stopwatch.Stop();

            Assert.Less(stopwatch.ElapsedMilliseconds, 5000);
        }

        [Test]
        public async Task PrimitiveSleep_CancelledPartway_ShouldReturnBeforeFullDuration()
        {
            using var cts = new CancellationTokenSource();
            cts.CancelAfter(200);

            var stopwatch = Stopwatch.StartNew();
            await HLTasks.PrimitiveSleep(30, cts.Token);
            stopwatch.Stop();

            Assert.Less(stopwatch.ElapsedMilliseconds, 5000);
        }
    }
}
