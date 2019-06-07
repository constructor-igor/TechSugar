using System;
using System.ServiceModel;
using Shavuot.Contract;

namespace Shavuot.Client
{
    public class CallBack : IShavuotServiceCallback
    {
        #region IShavuotServiceCallback
        public void OnNewMessage(Message message)
        {
            
        }
        #endregion
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[Shavuot.Client] started");
            Type callBackType = typeof(CallBack);
            DuplexChannelFactory<IShavuotService> cf = new DuplexChannelFactory<IShavuotService>(callBackType, "ShavuotServiceEndpoint");
            IShavuotService proxy = cf. CreateChannel(new InstanceContext(new CallBack()));

            string message = "";
            do
            {
                Console.Write("Message (<Enter> - finish): ");
                message = Console.ReadLine();
                if (message!="")
                    proxy.Greeting(new Message(message));
            } while (message != "");
        }
    }
}
