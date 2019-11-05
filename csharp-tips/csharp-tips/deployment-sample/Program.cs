using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/*
 *
 *  https://docs.microsoft.com/en-us/dotnet/api/system.deployment.application.applicationdeployment.updatelocation?redirectedfrom=MSDN&view=netframework-4.8#System_Deployment_Application_ApplicationDeployment_UpdateLocation
 */

namespace deployment_sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Version version = getRunningVersion();
            Console.WriteLine($"Version: {version}");
            Console.Write("press <Enter>");
            Console.ReadLine();
        }
        private static Version getRunningVersion()
        {
            try
            {
                return ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }
    }
}
