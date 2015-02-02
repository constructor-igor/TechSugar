using System;
using System.Diagnostics;
using System.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Task2.Consumer
{
    class Program
    {
        private const int SEC1 = 1000;
        const string queuePath = @".\Private$\MSMQ-Task2";
        static void Main()
        {
            Console.WriteLine("'Consumer' started");
            using (MessageQueue mq = MessageQueueHelper.GetMessageQueue(queuePath))
            {
                bool exit;
                do
                {
                    Task.Run(() =>
                    {
                        do
                        {
                            Message message = null;
                            try
                            {
                                message = mq.Receive(new TimeSpan(0, 0, seconds: 3));
                            }
                            catch (MessageQueueException)
                            {
                                Console.WriteLine("not found message in queue");
                            }
                            if (message != null)
                            {
                                ProducerMessage customerMessage = null;
                                try
                                {
                                    message.Formatter = new XmlMessageFormatter(new[] {typeof (ProducerMessage)});
                                    customerMessage = (ProducerMessage) message.Body;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("failed with '{0}'", e.Message);
                                }
                                if (customerMessage != null)
                                {
                                    Guid taskId = Guid.NewGuid();
                                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                                    Console.WriteLine("Received message '{0}, {1}'", customerMessage.Text, customerMessage.Duration);
                                    Console.WriteLine("{0} starting", taskId);
                                    Stopwatch stopwatch = new Stopwatch();
                                    stopwatch.Start();
                                    Thread.Sleep(customerMessage.Duration * SEC1);
                                    Console.WriteLine("{0} completed, duration = {1}", taskId, Math.Round(stopwatch.ElapsedMilliseconds / (double)(SEC1)));
                                    Console.ResetColor();
                                }
                            }
                        } while (true);
                    });
                    
                    //Console.Write("enter command or 'exit' > ");
                    string command = Console.ReadLine() ?? "";
                    exit = String.IsNullOrWhiteSpace(command) || command.ToLower() == "exit";
                } while (!exit);
            }
        }
    }
}
