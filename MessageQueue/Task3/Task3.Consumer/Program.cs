using System;
using System.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Task3.Consumer
{
    class Program
    {
        const string queuePath = @".\Private$\MSMQ-Task3";
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
                                    message.Formatter = new XmlMessageFormatter(new[] { typeof(ProducerMessage) });
                                    customerMessage = (ProducerMessage)message.Body;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("failed with '{0}'", e.Message);
                                }
                                if (customerMessage != null)
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                                    Console.WriteLine("Received message '{0}' with sleep processing time '{1}'", customerMessage.Text, customerMessage.Duration);
                                    Console.WriteLine("{0}: processing start", DateTime.Now);
                                    Thread.Sleep(customerMessage.Duration * 1000);
                                    Console.WriteLine("{0}: processing completed", DateTime.Now);
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
