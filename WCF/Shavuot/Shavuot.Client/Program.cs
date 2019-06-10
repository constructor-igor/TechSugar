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
            const string endPointName = "TCPIP"; //"ShavuotServiceEndpoint"; //"TCPIP";
            Console.WriteLine($"[Shavuot.Client] started with end-point {endPointName}");
            Type callBackType = typeof(CallBack);
            DuplexChannelFactory<IShavuotService> cf = new DuplexChannelFactory<IShavuotService>(callBackType, endPointName);
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
