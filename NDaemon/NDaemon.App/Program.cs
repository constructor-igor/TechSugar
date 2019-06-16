using System;

namespace NDaemon.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[NDaemon.App (Server)] starting.");
            ApplicationWatchdogDefinition applicationWatchdogDefinition = ApplicationWatchdogDefinitionBuilder.CreateFromConfiguration();
            using (ApplicationWatcher watcher = new ApplicationWatcher(applicationWatchdogDefinition))
            {
                Console.Write("To finish watchdog, press <Enter>.");
                Console.ReadLine();
            }

            Console.WriteLine("[NDaemon.App (Server)] finished.");
        }
    }
}
