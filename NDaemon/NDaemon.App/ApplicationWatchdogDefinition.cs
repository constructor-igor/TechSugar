using System.IO;

namespace NDaemon.App
{
    public class ApplicationWatchdogDefinition
    {
        public readonly string MonitoredApplication;
        public readonly string WorkingDirectory;
        public readonly int TimeIntervalMs;
        public readonly string FullMonitoredPath;
        public readonly string MonitoredProcessName;
        public bool FileExists => File.Exists(FullMonitoredPath);

        public ApplicationWatchdogDefinition(string monitoredApplication, string workingDirectory, int timeIntervalMs)
        {
            MonitoredApplication = monitoredApplication;
            WorkingDirectory = workingDirectory;
            TimeIntervalMs = timeIntervalMs;
            FullMonitoredPath = Path.Combine(WorkingDirectory, MonitoredApplication);
            MonitoredProcessName = Path.GetFileNameWithoutExtension(MonitoredApplication);
        }
    }
}