using System;
using System.Messaging;
using System.Threading.Tasks;

namespace Task2.Producer
{
    //
    //  One producer -> several consumers
    //  Message acknowledgment
    //  Message durability (Recoverable Messages)
    //  Transactions
    //  Fair dispatch
    //
    class Program
    {
        const string queuePath = @".\Private$\MSMQ-Task2";
        const string ackQueuePath = @".\private$\MSMQ-Task2-Ack";
        static void Main()
        {
            Console.WriteLine("'Producer' started.");
            using (MessageQueue ackMQ = MessageQueueHelper.GetMessageQueue(ackQueuePath))
            using (MessageQueue mq = MessageQueueHelper.GetMessageQueue(queuePath))
            {
                ackMQ.MessageReadPropertyFilter.CorrelationId = true;                
                Task.Run(() =>
                {
                    do
                    {
                        try
                        {
                            Message ackMessage = ackMQ.Receive(new TimeSpan(0, 0, seconds: 3));
                            if (ackMessage.MessageType == MessageType.Acknowledgment)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine("Message ack '{0}' received {1}", ackMessage.Acknowledgment, ackMessage.CorrelationId);
                                Console.ResetColor();
                            }
                        }
                        catch (MessageQueueException)
                        {
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    } while (true);
                });

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
                                    Body = customerObject,
                                    AdministrationQueue = new MessageQueue(ackQueuePath),
                                    AcknowledgeType = 
                                        AcknowledgeTypes.FullReachQueue | 
                                        AcknowledgeTypes.FullReceive,
//                                        AcknowledgeTypes.NegativeReceive | 
//                                        AcknowledgeTypes.NotAcknowledgeReachQueue | 
//                                        AcknowledgeTypes.NotAcknowledgeReceive | 
//                                        AcknowledgeTypes.PositiveArrival | 
//                                        AcknowledgeTypes.PositiveReceive,
                                    TimeToReachQueue = new TimeSpan(0, 0, 5),
                                    TimeToBeReceived = new TimeSpan(0, 0, 5)
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
