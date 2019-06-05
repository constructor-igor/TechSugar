using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Demo.Contracts;

namespace demo.client.console
{
    public class ServiceProxy : ClientBase<IHelloWorldService>
    {
        public ServiceProxy()
            : base(new ServiceEndpoint(ContractDescription.GetContract(typeof(IHelloWorldService)),
                new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/HelloWorldService")))
        {

        }

        public IHelloWorldService Service => Channel;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client started");            

            ServiceProxy proxy = new ServiceProxy();
            Console.WriteLine(proxy.Service.GetMessage("Jon"));
            proxy.Service.SendMessage(new TextHelloWorldMessage { Id=Guid.NewGuid(), TimeStamp = DateTime.Now, MessageType = MessageType.Text, MessageText = "test 1"});
            proxy.Service.SendMessage(new TextHelloWorldMessage { Id=Guid.NewGuid(), TimeStamp = DateTime.Now, MessageType = MessageType.Warning, MessageText = "test 2"});
            proxy.Service.SendMessage(new TextHelloWorldMessage { Id=Guid.NewGuid(), TimeStamp = DateTime.Now, MessageType = MessageType.Error, MessageText = "test 3"});

            proxy.Service.SendMessage(new DigitalHelloWorldMessage { Id = Guid.NewGuid(), TimeStamp = DateTime.Now, MessageType = MessageType.Error, MessageData = 1 });
            proxy.Service.SendMessage(new DigitalHelloWorldMessage { Id = Guid.NewGuid(), TimeStamp = DateTime.Now, MessageType = MessageType.Error, MessageData = 2 });
            proxy.Service.SendMessage(new DigitalHelloWorldMessage { Id = Guid.NewGuid(), TimeStamp = DateTime.Now, MessageType = MessageType.Error, MessageData = 3 });

            ConsoleKeyInfo key;
            int data = 10;
            do
            {                
                Console.WriteLine("Press any key to exit...");
                key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.N:
                        proxy.Service.SendMessage(new DigitalHelloWorldMessage { Id = Guid.NewGuid(), TimeStamp = DateTime.Now, MessageType = MessageType.Error, MessageData = data++ });
                        break;
                    case ConsoleKey.R:
                        for (int i = 0; i < 10; i++)
                        {
                            proxy.Service.SendMessage(new DigitalHelloWorldMessage { Id = Guid.NewGuid(), TimeStamp = DateTime.Now, MessageType = MessageType.Error, MessageData = data++ });
                        }
                        break;
                }
            } while (key.Key == ConsoleKey.N || key.Key == ConsoleKey.R);
        }
    }
}
