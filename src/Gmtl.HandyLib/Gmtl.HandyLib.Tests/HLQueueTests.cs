using System;
using NUnit.Framework;

namespace Gmtl.HandyLib.Tests
{
    [TestFixture]
    public class HLQueueTests
    {
        [Test]
        public void Enqueue_ShouldIncreaseCount_AndRaiseEnqueuedEvent()
        {
            var queue = new HLQueue<string>();
            bool enqueuedRaised = false;

            queue.Enqueued += (_, _) => enqueuedRaised = true;

            queue.Enqueue("first");

            Assert.That(queue.Count, Is.EqualTo(1));
            Assert.That(enqueuedRaised, Is.True);
        }

        [Test]
        public void Dequeue_ShouldReturnItemsInFifoOrder_AndRaiseDequeuedEvent()
        {
            var queue = new HLQueue<int>();
            bool dequeuedRaised = false;

            queue.Dequeued += (_, _) => dequeuedRaised = true;

            queue.Enqueue(10);
            queue.Enqueue(20);

            int dequeuedItem = queue.Dequeue();

            Assert.That(dequeuedItem, Is.EqualTo(10));
            Assert.That(queue.Count, Is.EqualTo(1));
            Assert.That(dequeuedRaised, Is.True);
        }

        [Test]
        public void Dequeue_OnEmptyQueue_ShouldThrowInvalidOperationException()
        {
            var queue = new HLQueue<object>();

            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }
    }
}