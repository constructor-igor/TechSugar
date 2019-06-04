using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

/*
 *
 * https://medium.com/@saurabh.singh0829/c-parallel-programming-part-1-producer-consumer-pattern-7222c6d29736
 *
 */

namespace ConsumerProducerSingleProcess
{
    public class BackgroundColor : IDisposable
    {
        private readonly ConsoleColor m_originalColor;

        public BackgroundColor(ConsoleColor color)
        {
            m_originalColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
        }
        #region IDisposable
        public void Dispose()
        {
            Console.BackgroundColor = m_originalColor;
        }
        #endregion
    }
    public class Message
    {
        public readonly string Id;

        public Message(string id)
        {
            Id = id;
        }
    }
    class Program
    {
        static readonly BlockingCollection<Message> messages = new BlockingCollection<Message>();

        static readonly object m_locked = new object();
        static void PrintMessage(string message, ConsoleColor color)
        {
            lock (m_locked)
            {
                using (new BackgroundColor(color))
                {
                    Console.WriteLine(message);
                }
            }
        }
        static void Main(string[] args)
        {
            Task producer = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Message message = new Message($"{i}");
                    messages.Add(message);
                    PrintMessage($"{DateTime.Now} Created message {message.Id}", ConsoleColor.DarkYellow);
                    Thread.Sleep(1000);
                }
            });

            Task consumer = Task.Factory.StartNew(action: () =>
            {
                while (true)
                {
                    Message message = messages.Take();
                    PrintMessage($"[{Thread.CurrentThread.ManagedThreadId}] {DateTime.Now} Processed message {message.Id}", ConsoleColor.DarkGreen);
                    Thread.Sleep(2000);
                }
            });

            producer.Wait();
            consumer.Wait();
        }
    }
}
