using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using log4net;

namespace Guidelines.Core.Threading
{
    public class ProducerConsumerQueue
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly object _locker = new object();
        private readonly Thread[] _workers;
        private readonly Queue<Action> _itemQ = new Queue<Action>();

        public ProducerConsumerQueue(int workerCount)
        {
            _workers = new Thread[workerCount];

            // Create and start a separate thread for each worker
            for (int i = 0; i < workerCount; i++)
            {
                (_workers[i] = new Thread(Consume)).Start();
            }
        }

        public void Shutdown(bool waitForWorkers)
        {
            // Enqueue one null item per worker to make each exit.
            foreach (Thread worker in _workers)
            {
                EnqueueItem(null);
            }

            // Wait for workers to finish
            if (waitForWorkers)
            {
                foreach (Thread worker in _workers)
                {
                    worker.Join();
                }
            }
        }

        public void EnqueueItem(Action item)
        {
            lock (_locker)
            {
                _itemQ.Enqueue(item);           // We must pulse because we're
                Monitor.Pulse(_locker);         // changing a blocking condition.
            }
        }

        void Consume()
        {
            while (true)                        // Keep consuming until
            {                                   // told otherwise.
                Action item;
                lock (_locker)
                {
                    while (_itemQ.Count == 0)
                    {
                        Monitor.Wait(_locker);
                    }
                    item = _itemQ.Dequeue();
                }

                if (item == null)
                {
                    return;         // This signals our exit.
                }

                try
                {
                    item();         // Execute item.
                }
                catch (Exception e)
                {
                    _logger.Error("Error Processing thread", e);
                }
            }
        }
    }
}
