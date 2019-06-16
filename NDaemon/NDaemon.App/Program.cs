using System;
using NDaemon.Framework.Infra;
using NDaemon.Framework.Watchdog;
using NLog;

namespace NDaemon.App
{
    class Program
    {
        private static readonly Logger m_logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            m_logger.Info("");
            m_logger.Info("[NDaemon.App (Server)] starting.");
            ApplicationWatchdogDefinition applicationWatchdogDefinition = ApplicationWatchdogDefinitionBuilder.CreateFromConfiguration();
            using (ApplicationWatcher watcher = new ApplicationWatcher(applicationWatchdogDefinition))
            {
                using (new BackGroundColorSetting(ConsoleColor.DarkGreen))
                {
                    Console.WriteLine("To finish watchdog, press <Enter>.");
                }
                Console.ReadLine();
            }

            m_logger.Info("[NDaemon.App (Server)] finished.");
        }
    }
}
