using System;
using System.Collections.Generic;

namespace Gmtl.HandyLib
{
    /// <summary>
    /// Queue with Enqueue/Dequeue events
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HLQueue<T>
    {
        private readonly Queue<T> queue = new Queue<T>();
        public event EventHandler Enqueued;
        public event EventHandler Dequeued;

        protected virtual void OnEnqueued()
        {
            if (Enqueued == null)
            {
                return;
            }

            Enqueued(this, EventArgs.Empty);
        }

        protected virtual void OnDequeued()
        {
            if (Dequeued == null)
            {
                return;
            }

            Dequeued(this, EventArgs.Empty);
        }

        public virtual void Enqueue(T item)
        {
            queue.Enqueue(item);
            OnEnqueued();
        }

        public int Count => queue.Count;

        public virtual T Dequeue()
        {
            T item = queue.Dequeue();
            OnDequeued();

            return item;
        }
    }
}