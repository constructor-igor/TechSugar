using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class QueueSamples
    {
        [Test]
        public void Test()
        {
            Queue<int> queue = new Queue<int>();

            queue.Enqueue(10);
            queue.Enqueue(20);

            while (queue.Any())
            {
                Console.WriteLine("queue.Dequeue: " + queue.Dequeue());
            }

            Assert.Pass();
        }

        [Test]
        public void TestInMultiThreadedMode()
        {
            using (ProducerConsumerQueue producerConsumerQueue = new ProducerConsumerQueue())
            {
                producerConsumerQueue.EnqueueTask("task1");
                producerConsumerQueue.EnqueueTask("task2");
                producerConsumerQueue.EnqueueTask("task3");
                Console.WriteLine("main thread: before sleep(1000)");
                Thread.Sleep(1000);
                Console.WriteLine("main thread: after sleep(1000)");
                producerConsumerQueue.EnqueueTask("task4");
                producerConsumerQueue.EnqueueTask("task5");
                producerConsumerQueue.EnqueueTask("task6");
                Thread.Sleep(1000);
                producerConsumerQueue.IsRun = false;
                Thread.Sleep(1000);
            }
            Assert.Pass();
        }
    }

    //
    // http://www.albahari.com/threading/part2.aspx#_ProducerConsumerQWaitHandle
    //
    public class ProducerConsumerQueue : IDisposable
    {
        private readonly EventWaitHandle m_wh = new AutoResetEvent(false);
        private readonly Thread m_worker;
        private readonly object m_locker = new object();
        private readonly Queue<string> m_tasks = new Queue<string>();
        private bool m_IsRun;

        public bool IsRun
        {
            get { return m_IsRun; }
            set
            {
                m_IsRun = value; 
                m_wh.Set();
            }
        }

        public ProducerConsumerQueue()
        {
            IsRun = true;
            m_worker = new Thread(Work);
            m_worker.Start();
        }

        public void EnqueueTask(string task)
        {
            lock (m_locker) m_tasks.Enqueue(task);
            m_wh.Set();
        }

        void Work()
        {
            Console.WriteLine("[start] Work");
            while (m_IsRun)
            {
                string task = null;
                lock (m_locker)
                if (m_tasks.Count > 0)
                {
                    task = m_tasks.Dequeue();
                    if (task == null) return;
                }
                if (task != null)
                {
                    Console.WriteLine ("Performing task: " + task);
                    Thread.Sleep (100);  // simulate work...
                } else
                m_wh.WaitOne();         // No more tasks - wait for a signal            
            }
            Console.WriteLine("[start] Finish");
        }

        #region IDisposable
        public void Dispose()
        {
            Console.WriteLine("[start] Dispose()");
            m_worker.Join();    // Wait for the consumer's thread to finish.
            m_wh.Close();       // Release any OS resources.
            Console.WriteLine("[finish] Dispose()");
        }
        #endregion
    }
}