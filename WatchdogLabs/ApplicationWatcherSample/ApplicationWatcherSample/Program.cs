
/*
 * Reference: https://www.codeproject.com/Tips/1054098/Simple-Csharp-Watchdog
 *
 *
 */

namespace ApplicationWatcher.Watchdog
{
    class Program
    {
        static void Main(string[] args)
        {
            string workingFolder = @"D:\My\@gh\TechSugar\WatchdogLabs\ApplicationWatcherSample\ApplicationWatcherSample.Client\bin\Debug";
            string monitoredApplicationName = "ApplicationWatcherSample.Client";
            ApplicationWatcher applicationWatcher = new ApplicationWatcher(workingFolder, monitoredApplicationName, "WatchDog", 5000);
        }
    }
}
