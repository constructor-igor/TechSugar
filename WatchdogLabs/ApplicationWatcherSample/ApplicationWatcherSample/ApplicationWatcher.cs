using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ApplicationWatcher.Watchdog
{
    public class ApplicationWatcher
    {
        private const string HEARTBEAT_FILE_NAME = "heartbeat";

        private readonly string m_workingDirectory;
        private string m_monitoredAppName = "MonitoredApplication";
        private string watchdogAppName = "WatchDog";
        private int monitoringInterval = 5000;
        private readonly bool m_heartbeatEnabled = false;

        /// <summary>
        /// The ApplicationWatcher class takes in the monitored application name, watchdog application name and the preferred monitoring interval and initiates the watchdog process.
        /// </summary>
        /// <param name="monitoredApplicationName">Name of the application to be monitored</param>
        /// <param name="watchdogApplicationName">Name of the watchdog application</param>
        /// <param name="monitoringInterval">Monitoring interval in Milliseconds</param>
        public ApplicationWatcher(string workingDirectory, string monitoredApplicationName, string watchdogApplicationName, int monitoringInterval)
        {
            m_workingDirectory = workingDirectory;
            this.m_monitoredAppName = monitoredApplicationName;
            this.watchdogAppName = watchdogApplicationName;
            this.monitoringInterval = monitoringInterval;

            // Check if another instance of this application is running
            // If True self-terminate
            // Else continue
            try
            {
                Process[] pname = Process.GetProcessesByName(watchdogAppName);

                if (pname.Length > 1)
                {
                    Process.GetCurrentProcess().Kill();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ApplicationWatcher Exception1: " + ex.StackTrace);
            }

            try
            {
                Thread watchDogThread = new Thread(StartAppMonitoring);
                watchDogThread.Start();
            }
            catch (Exception ex)
            {
            }
        }

        ///<summary>
        /// Utility method to check if a process belonging to the monitored application is running
        /// </summary>
        /// <returns> Bool
        /// True : A monitored app process exists
        /// False : A monitored app process does not exist
        /// </returns>
        private bool MonitoredAppExists()
        {
            try
            {
                Process[] processList = Process.GetProcessesByName(m_monitoredAppName);
                return processList.Length != 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ApplicationWatcher MonitoredAppExists Exception: " + ex.StackTrace);
                return true;
            }
        }


        ///<summary>
        /// Worker method of the Watchdog application
        /// Monitors the relevant application process availability
        /// Restarts the application if the process is not available
        /// </summary>
        private void StartAppMonitoring()
        {
            while (true)
            {
                // A lock file is used to temporarily disable the watchdog from monitoring and reviving the monitored application for debugging purposes.
                if (!File.Exists("watchdoglock"))
                {
                    string monitoredAppExePath = m_monitoredAppName + ".exe";
                    if (!MonitoredAppExists())
                    {
                        if (File.Exists(Path.Combine(m_workingDirectory, monitoredAppExePath)))
                        {
                            ProcessStartInfo startInfo = new ProcessStartInfo(monitoredAppExePath);
                            startInfo.WorkingDirectory = m_workingDirectory;
                            Process.Start(startInfo);
                        }
                        else
                        {
                            Debug.WriteLine("ApplicationWatcher StartAppMonitoring Monitored Application exe not found at: " + monitoredAppExePath);
                        }
                    }
                    else
                    {
                        if (m_heartbeatEnabled)
                            CheckHeartbeat(monitoredAppExePath);
                    }
                }
                else
                {
                    Debug.WriteLine("ApplicationWatcher StartAppMonitoring watchdoglock found.");
                }
                Thread.Sleep(monitoringInterval);
            }
        }

        private void CheckHeartbeat(string monitoredAppExePath)
        {
            if (File.Exists(HEARTBEAT_FILE_NAME))
            {
                try
                {
                    File.Delete(HEARTBEAT_FILE_NAME);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("ApplicationWatcher StartAppMonitoring Exception1: " + ex.StackTrace);
                }
            }
            else
            {
                // If the heartbeat file is not created, this could mean that the Monitored Application could be frozen
                while (MonitoredAppExists())
                {
                    try
                    {
                        Process[] pname = Process.GetProcessesByName(m_monitoredAppName);

                        foreach (Process process in pname)
                        {
                            process.Kill();
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("ApplicationWatcher StartAppMonitoring Exception2: " + ex.StackTrace);
                    }
                }

                Process.Start(monitoredAppExePath);
            }
        }
    }
}