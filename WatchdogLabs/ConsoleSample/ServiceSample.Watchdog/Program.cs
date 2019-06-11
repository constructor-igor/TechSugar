using System.ServiceProcess;

namespace ServiceSample.Watchdog
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun = new ServiceBase[]
            {
                new ServiceWatchdog()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
