using System;
using System.ServiceModel;
using Shavuot.Service;

namespace Shavuot.ServiceHostingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(ShavuotService)))
            {
                host.Open();

                Console.WriteLine("[Shavuot.ServiceHostingApp] Service started");
                Console.WriteLine("To exit, press <Enter>");
                Console.ReadLine();
            }
        }
    }
}
