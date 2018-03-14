using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp1
{
    class MySuperQueue<T> : IDisposable
    {
        private readonly object _popLocker = new object();
        private readonly object _queueLocker = new object();
        private readonly ManualResetEvent _signal = new ManualResetEvent(false);
        private readonly Queue<T> _queue = new Queue<T>();

        public void Push(T item)
        {
            lock (_queueLocker)
            {
                _queue.Enqueue(item);
                _signal.Set();
            }
        }

        public T Pop()
        {
            lock (_popLocker)
            {
                _signal.WaitOne();
                lock (_queueLocker)
                {
                    var item = _queue.Dequeue();
                    if (_queue.Count == 0)
                    {
                        _signal.Reset();
                    }

                    return item;
                }
            }
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _signal.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
