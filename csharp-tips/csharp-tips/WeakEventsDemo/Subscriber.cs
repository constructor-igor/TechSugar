using System;

namespace WeakEventsDemo
{
    public class Subscriber
    {
        private readonly string m_id;
        public Subscriber(string id)
        {
            m_id = id;
            Console.WriteLine($"[{m_id}, {GetHashCode()}] Subscriber.ctor()");
        }
        public void EventProcessing(object sender, EventArgs args)
        {
            Console.WriteLine($"[{m_id}, {GetHashCode()}] [event] args={args}");
        }
        ~Subscriber()
        {
            Console.WriteLine($"[{m_id}, {GetHashCode()}] Subscriber.finalizer()");
        }
    }
}