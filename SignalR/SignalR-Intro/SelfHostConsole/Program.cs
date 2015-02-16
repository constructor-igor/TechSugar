using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
/*
 * 
 * References:
 * http://www.asp.net/signalr/overview/deployment/tutorial-signalr-self-host
 * http://metanit.com/sharp/mvc5/16.2.php
 * http://stackoverflow.com/questions/16004375/signalr-how-to-call-net-client-method-from-server
 * */
using Owin;

namespace SelfHostConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // This will *ONLY* bind to localhost, if you want to bind to all addresses
            // use http://*:8080 or http://+:8080 to bind to all addresses. 
            // See http://msdn.microsoft.com/en-us/library/system.net.httplistener.aspx 
            // for more information.

            using (WebApp.Start<Startup>("http://localhost:8080/"))
            {
                Console.WriteLine("Server running at http://localhost:8080/");
                Console.ReadLine();

                IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MyHub>();

                string message;
                do
                {
                    Console.Write("Enter message: ");
                    message = Console.ReadLine();
                    if (!string.IsNullOrEmpty(message))
                    {
                        context.Clients.All.addMessage("server", message);
                    }
                } while (!string.IsNullOrEmpty(message));

                Console.WriteLine("to close the serve, please, press Enter");
                Console.ReadLine();
            }
        }
    }

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
    public class MyHub : Hub
    {
        public void Send(string name, string message)
        {
            Console.WriteLine("{0}: {1}", name, message);
            Clients.All.addMessage(name, message);
        }
    }
}
