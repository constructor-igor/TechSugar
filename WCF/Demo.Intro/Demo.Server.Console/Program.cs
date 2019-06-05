using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading;
using System.Threading.Tasks;
using Demo.Contracts;

namespace Demo.Server.Console
{
    [ServiceBehavior]
    public class HelloWorldService : IHelloWorldService
    {
        private readonly MessagesStorage m_messagesStorage;
        private readonly MessageProcessing m_messageProcessing;
        private readonly MessageConverter m_messageConverter;
        private readonly MessageController m_messageController;
        private readonly LogWriter m_logWriter;

        public HelloWorldService()
        {
            m_messagesStorage = new MessagesStorage();
            m_messageConverter = new MessageConverter();
            m_messageController = new MessageController();
            m_logWriter = new LogWriter();
            m_messageProcessing = new MessageProcessing(m_messagesStorage, m_messageConverter, m_messageController, m_logWriter);
        }
        #region IHelloWorldService
        [OperationBehavior]
        public string GetMessage(string name)
        {
            return "Hello world from " + name + "!";
        }

        public void SendMessage(HelloWordMessage message)
        {
            m_messagesStorage.AddMessage(message);
            m_messageController.Set();
        }
        #endregion
    }

    public class MessageController
    {
        static readonly AutoResetEvent m_autoResetEvent = new AutoResetEvent(true);

        public void Set()
        {
            m_autoResetEvent.Set();
        }

        public void Wait()
        {
            m_autoResetEvent.WaitOne();
        }
    }

    public class MessagesStorage
    {
        private readonly ConcurrentQueue<HelloWordMessage> m_queue;
        public MessagesStorage()
        {
            m_queue = new ConcurrentQueue<HelloWordMessage>();
        }
        public void AddMessage(HelloWordMessage message)
        {
            m_queue.Enqueue(message);
            System.Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] added message {message.Id}");
        }

        public HelloWordMessage Take()
        {
            HelloWordMessage message;
            if (m_queue.TryDequeue(out message))
                return message;
            return null;
        }
    }

    public class MessageProcessing
    {
        private readonly MessagesStorage m_messagesStorage;
        private readonly MessageConverter m_messageConverter;
        private readonly LogWriter m_logWriter;
        private readonly MessageController m_messageController;

        public MessageProcessing(MessagesStorage messagesStorage, MessageConverter messageConverter, MessageController messageController, LogWriter logWriter)
        {
            m_messagesStorage = messagesStorage;
            m_messageConverter = messageConverter;
            m_messageController = messageController;
            m_logWriter = logWriter;
            Task.Run(() => ProcessMessage());
        }

        private void ProcessMessage()
        {
            while (true)
            {
                HelloWordMessage message = m_messagesStorage.Take();
                if (message == null)
                {
                    //Thread.Sleep(50);
                    m_messageController.Wait();
                }
                else
                {
                    try
                    {
                        System.Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] process message {message.Id}");
                        string text = m_messageConverter.ConvertMessageToText(message);
                        System.Console.WriteLine(text);
                        m_logWriter.Log(text);
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine($"Exception {e.Message} in {e.StackTrace}");
                    }
                }
            }
        }
    }

    public class MessageConverter
    {
        public string ConvertMessageToText(HelloWordMessage message)
        {
            TextHelloWorldMessage textMessage = message as TextHelloWorldMessage;
            if (textMessage!=null)
                return $"[{textMessage.TimeStamp}][{textMessage.MessageType}] {textMessage.Id}: {textMessage.MessageText}";
            DigitalHelloWorldMessage digitalMessage = message as DigitalHelloWorldMessage;
            if (digitalMessage!=null)
                return $"[{digitalMessage.TimeStamp}][{digitalMessage.MessageType}] {digitalMessage.Id}: {digitalMessage.MessageData}";
            return $"unknown message {message.GetType()}";
        }
    }

    public class LogWriter
    {
        private readonly string m_logFilePath;
        public LogWriter()
        {
            string executingFile = Assembly.GetExecutingAssembly().Location;
            string executingDirectory = Path.GetDirectoryName(executingFile);
            m_logFilePath = Path.Combine(executingDirectory, "messagesLog.txt");
        }
        public void Log(string message)
        {
            CreateFile();
            using (var writer = new StreamWriter(m_logFilePath, true))
            {
                writer.WriteLine(message);
            }
        }

        private void CreateFile()
        {
            if (!File.Exists(m_logFilePath))
            {
                using (File.Create(m_logFilePath))
                {
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = CreateServiceHost(typeof(Demo.Contracts.IHelloWorldService), typeof(Demo.Server.Console.HelloWorldService));
            //ServiceHost host = new ServiceHost(typeof(Demo.Server.Console.HelloWorldService));

            host.Open();
            System.Console.WriteLine("Service Hosted Successfully");
            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey();
        }

        public static ServiceHost CreateServiceHost(Type serviceInterface, Type implementation)
        {
            //Create base address
            string baseAddress = "net.pipe://localhost/HelloWorldService";

            ServiceHost serviceHost = new ServiceHost(implementation, new Uri(baseAddress));

            //Net named pipe
            NetNamedPipeBinding binding = new NetNamedPipeBinding { MaxReceivedMessageSize = 2147483647 };
            serviceHost.AddServiceEndpoint(serviceInterface, binding, baseAddress);

            //MEX - Meta data exchange
            ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            serviceHost.Description.Behaviors.Add(behavior);
            serviceHost.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexNamedPipeBinding(), baseAddress + "/mex/");

            return serviceHost;
        }
    }
}
