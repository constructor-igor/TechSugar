using System;
using System.ServiceModel;
using Shavuot.Contract;

namespace Shavuot.Viewer
{
    public class ShavuotViewer: IShavuotServiceCallback
    {
        #region IShavuotServiceCallback
        public void OnNewMessage(Message message)
        {
            Console.WriteLine($"[ShavuotViewer ({GetHashCode()}]: {message}");
        }
        #endregion
    }
    class Program
    {
        static void Main(string[] args)
        {
            const string endPointName = "TCPIP"; //"ShavuotServiceEndpoint"; //"TCPIP";
            Console.WriteLine($"[Shavuot.Viewer] started with end-point {endPointName}");

            IShavuotServiceCallback viewer = new ShavuotViewer();
            Type callBackType = typeof(ShavuotViewer);
            DuplexChannelFactory<IShavuotService> cf = new DuplexChannelFactory<IShavuotService>(viewer, endPointName);
            IShavuotService proxy = cf.CreateChannel(new InstanceContext(viewer));

            bool subscribed = proxy.Subscribe();
            Console.WriteLine($"[Shavuot.Viewer] subscribed: {subscribed}");

            Console.Write("Press <Enter> to exit: ");
            Console.ReadLine();

            bool unSubscribed = proxy.Unsubscribe();
            Console.WriteLine($"[Shavuot.Viewer] unSubscribed: {unSubscribed}");
        }
    }
}
