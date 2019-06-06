using System;
using System.ServiceModel;
using Shavuot.Contract;

namespace Shavuot.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[Shavuot.Client] started");
            ChannelFactory<IShavuotService> cf = new ChannelFactory<IShavuotService>("ShavuotServiceEndpoint");
            IShavuotService proxy = cf.CreateChannel();

            string message = "";
            do
            {
                Console.Write("Message (<Enter> - finish): ");
                message = Console.ReadLine();
                if (message!="")
                    proxy.Greeting(message);
            } while (message != "");
        }
    }
}
