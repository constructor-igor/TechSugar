using System;
using System.Diagnostics;
using System.Threading;
using NLog;

namespace NDaemon.Framework.Watchdog
{
    public class ApplicationWatcher: IDisposable
    {
        private static readonly Logger m_logger = LogManager.GetCurrentClassLogger();
        private readonly ApplicationWatchdogDefinition m_applicationDefinition;
        readonly CancellationTokenSource m_cancellationTokenSource = new CancellationTokenSource();

        public ApplicationWatcher(ApplicationWatchdogDefinition applicationDefinition)
        {
            m_applicationDefinition = applicationDefinition;
            Thread watchDogThread = new Thread(StartAppMonitoring);
            watchDogThread.Start();
            m_logger.Info($"Starting Application Watcher for {applicationDefinition.FullMonitoredPath}");
        }
        private void StartAppMonitoring()
        {
            while (!m_cancellationTokenSource.IsCancellationRequested)
            {
                bool monitoredApplicationExists = MonitoredAppExists(m_applicationDefinition.MonitoredProcessName);
                if (!monitoredApplicationExists)
                {
                    m_logger.Info($"Monitoed process {m_applicationDefinition.MonitoredProcessName} not found.");
                    if (m_applicationDefinition.FileExists)
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo(m_applicationDefinition.MonitoredApplication)
                        {
                            WorkingDirectory = m_applicationDefinition.WorkingDirectory
                        };
                        Process.Start(startInfo);
                        m_logger.Info($"Started monitored application {m_applicationDefinition.FullMonitoredPath}");
                    }
                }
                Thread.Sleep(m_applicationDefinition.TimeIntervalMs);
            }
            m_logger.Info($"Finihsed Application Watcher for {m_applicationDefinition.FullMonitoredPath}");
        }
        private bool MonitoredAppExists(string processName)
        {
            try
            {
                Process[] processList = Process.GetProcessesByName(processName);
                return processList.Length != 0;
            }
            catch (Exception ex)
            {
                m_logger.Warn(ex, "MonitoredAppExists failed.");
                return true;
            }
        }

        #region IDisposable
        public void Dispose()
        {
            m_cancellationTokenSource.Cancel();
        }
        #endregion
    }
}