using System;
using Nancy;
using Nancy.Hosting.Self;

namespace SelfHostingSample
{
    class Program
    {
        static void Main()
        {
            var url = "http://127.0.0.1:9000";
            //UrlReservations urlReservations = new UrlReservations {CreateAutomatically = true};
            HostConfiguration hostConfiguration = new HostConfiguration {UrlReservations = {CreateAutomatically = true}};
            using (var host = new NancyHost(new Uri(url), new DefaultNancyBootstrapper(), hostConfiguration))
            {
                host.Start();
                Console.WriteLine("Nancy Server listening on {0}", url);
                Console.ReadLine();
            }
        }
    }
}
