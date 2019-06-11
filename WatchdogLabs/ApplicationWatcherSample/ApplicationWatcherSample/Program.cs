
/*
 * Reference: https://www.codeproject.com/Tips/1054098/Simple-Csharp-Watchdog
 *
 *
 */

using System;
using System.IO;

namespace ApplicationWatcher.Watchdog
{
    class Program
    {
        static void Main(string[] args)
        {
            string workingFolder = @"..\..\..\ApplicationWatcherSample.Client\bin\Debug";
            string monitoredApplicationName = "ApplicationWatcherSample.Client";
            string fullPath = Path.GetFullPath(Path.Combine(workingFolder, monitoredApplicationName+".exe"));
            if (!File.Exists(fullPath))
            {
                Console.WriteLine($"Not found monitored application {fullPath}");
                return;
            }
            ApplicationWatcher applicationWatcher = new ApplicationWatcher(workingFolder, monitoredApplicationName, "WatchDog", 5000);
        }
    }
}
