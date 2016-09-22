using System;
using Nancy.Bootstrapper;
using Nancy.Hosting.Self;

namespace SimpleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://127.0.0.1:9664";
            HostConfiguration hostConfiguration = new HostConfiguration { UrlReservations = { CreateAutomatically = true } };
            //INancyBootstrapper defaultNancyBootstrapper = new DefaultNancyBootstrapper();
            INancyBootstrapper defaultNancyBootstrapper = new CustomBootstrapper();
            using (var host = new NancyHost(new Uri(url), defaultNancyBootstrapper, hostConfiguration))
            {
                host.Start();
                Console.WriteLine("Nancy Server listening on {0}", url);
                Console.ReadLine();
            }
        }
    }
}
