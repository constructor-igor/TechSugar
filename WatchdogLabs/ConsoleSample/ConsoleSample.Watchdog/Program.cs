using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

/*
 * Reference: https://stackoverflow.com/questions/11146381/whats-the-best-way-to-watchdog-a-desktop-application
 *
 *
 */

namespace ConsoleSample.Watchdog
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[ConsoleSample.Watchdog] Started");
            if (!args.Any())
            {
                Console.WriteLine("[ConsoleSample.Watchdog] Requires path to controlled application (.exe).");
                Console.WriteLine("[ConsoleSample.Watchdog] Failed");
            }
            string controlledClientApplication = args[0];
            Console.WriteLine($"Controlled client application: {controlledClientApplication}");
            if (File.Exists(controlledClientApplication))
            {
                string workingFolder = Path.GetDirectoryName(controlledClientApplication);
                var process = new Process();
                process.StartInfo = new ProcessStartInfo { FileName = controlledClientApplication, Arguments = "-arg1 -arg2" };
                process.StartAsActiveUser();
            }
            else
            {
                Console.WriteLine($"Application {controlledClientApplication} not found.");
            }
            Console.WriteLine("[ConsoleSample.Watchdog] Finished");
        }
    }
}
