using System;
using System.Threading;

namespace WeakEventsDemo
{
    public class Publisher
    {
        private EventHandler m_myEvent; // the underlying field
        // this isn't actually named "_MyEvent" but also "MyEvent",
        // but then you couldn't see the difference between the field
        // and the event.
        public event EventHandler MyEvent
        {
            add { lock (this) { m_myEvent += value; } }
            remove { lock (this) { m_myEvent -= value; } }
        }

        public Publisher()
        {
            Console.WriteLine($"[{GetHashCode()}] Publisher.ctor()");
        }
        public void Run()
        {
            Console.WriteLine($"[{GetHashCode()}] Publisher.Run() started");
            m_myEvent?.Invoke(this, EventArgs.Empty);
            Thread.Sleep(2 * 1000);
            m_myEvent?.Invoke(this, EventArgs.Empty);
            Console.WriteLine($"[{GetHashCode()}] Publisher.Run() finished");
        }

        ~Publisher()
        {
            Console.WriteLine($"[{GetHashCode()}] Publisher.finalizer()");
        }
    }
}