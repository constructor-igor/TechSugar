using System;
using System.Configuration;

namespace NDaemon.App
{
    public class ApplicationWatchdogDefinitionBuilder
    {
        public static ApplicationWatchdogDefinition CreateFromConfiguration()
        {
            string monitoredApplication = GetSettingValue("monitoredApplication");
            string workingDirectory = GetSettingValue("monitoredWorkingDirectory");
            int timeIntervalMs = Convert.ToInt32(GetSettingValue("monitoredTimeIntervalMs"));
            return new ApplicationWatchdogDefinition(monitoredApplication, workingDirectory, timeIntervalMs);
        }

        private static string GetSettingValue(string paramName)
        {
            return string.Format(ConfigurationManager.AppSettings[paramName]);
        }

    }
}