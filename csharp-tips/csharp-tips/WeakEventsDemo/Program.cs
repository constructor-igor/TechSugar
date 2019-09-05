using System;

namespace WeakEventsDemo
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("[Main] started");

            Console.WriteLine("[Main] DemoPublisher_NoEvent");
            DemoPublisher_NoEvent();
            Console.WriteLine("[Main] DemoPublisher_Event_LocalSubscriber");
            DemoPublisher_Event_LocalSubscriber();
            Console.WriteLine("[Main] DemoPublisher_Event_GlobalSubscriber");
            DemoPublisher_Event_GlobalSubscriber();

            Console.WriteLine("[Main] Demo finished, Let's call GC");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.WriteLine("[Main] finished");
        }

        private static void DemoPublisher_NoEvent()
        {
            Publisher publisher = new Publisher();
            publisher.Run();
        }
        private static void DemoPublisher_Event_LocalSubscriber()
        {
            Subscriber subscriber = new Subscriber("local");
            Publisher publisher = new Publisher();
            publisher.MyEvent += subscriber.EventProcessing;
            publisher.Run();
        }
        private static readonly Subscriber m_globalSubscriber = new Subscriber("global");
        private static void DemoPublisher_Event_GlobalSubscriber()
        {
            Publisher publisher = new Publisher();
            publisher.MyEvent += m_globalSubscriber.EventProcessing;
            publisher.Run();
        }
    }
}
