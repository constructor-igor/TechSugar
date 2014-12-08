using System;
using System.Messaging;

namespace Task1.Producer
{
    class Program
    {
        const string queuePath = @".\Private$\MSMQ-Task1";
        static void Main()
        {
            Console.WriteLine("'Producer' started.");
            using (MessageQueue mq = MessageQueueHelper.GetMessageQueue(queuePath))
            {
                bool exit;
                do
                {
                    Console.Write("enter command or 'exit' > ");
                    string command = Console.ReadLine() ?? "";
                    string[] commands = command.Split(' ');

                    try
                    {
                        switch (commands[0])
                        {
                            case "send":
                                var customerObject = new ProducerMessage(commands[1], Int32.Parse(commands[2]));
                                var message = new Message
                                {
                                    Priority = MessagePriority.Normal,
                                    Label = "",
                                    Body = customerObject
                                };
                                mq.Send(message);
                                Console.WriteLine("sent message '{0}, {1}'", customerObject.Text, customerObject.Duration);
                                break;
                        }
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine("failed with '{0}' message", e.Message);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("check command '{0}' arguments", commands[0]);
                    }
                    exit = String.IsNullOrWhiteSpace(command) || command.ToLower() == "exit";
                } while (!exit);
            }
        }
    }
}
