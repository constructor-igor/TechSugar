using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ApplicationWatcher.Watchdog
{
    public class WatchdogWatcher
    {
        private const string HEARTBEAT_FILE_NAME = "heartbeat";
        private string watchdogAppName = "WatchDog";
        private string watchdogExePath = "WatchDog.exe";
        private int watchDogMonitorInterval = 5000;

        /// <summary>
        /// The WatchdogWatcher class takes in the watchdog application name, watchdog executable path name and the preffered monitoring interval and initiates the watchdog watcher.
        /// </summary>
        /// <param name="watchdogApplicationName">Name of the watchdog application</param>
        /// <param name="watchdogExecutablePath">Path of the watchdog executale file</param>
        /// <param name="watchdogMonitoringInterval">MOnitoring interval in Milliseconds</param>
        public WatchdogWatcher(string watchdogApplicationName, string watchdogExecutablePath, int watchdogMonitoringInterval)
        {
            this.watchdogAppName = watchdogApplicationName;
            this.watchdogExePath = watchdogExecutablePath;
            this.watchDogMonitorInterval = watchdogMonitoringInterval;

            try
            {
                Thread watchDogManagerThread = new Thread(new ThreadStart(StartWatchDogMonitoring));
                watchDogManagerThread.Start();

                Thread heatbeatThread = new Thread(new ThreadStart(StartHeartbeatThread));
                heatbeatThread.Start();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception WatchdogMonitor2: " + ex.StackTrace);
            }
        }

        /// <summary>
        /// Worker method of the watchdog monitoring thread running on the Monitoder application
        /// Chechks for the watchdoglock and proceeds with the monitoring
        /// Revives the watchdog process if not running 
        /// </summary>
        private void StartWatchDogMonitoring()
        {
            while (true)
            {
                try
                {
                    if (!File.Exists("watchdoglock"))
                    {
                        Process[] processList = Process.GetProcessesByName(watchdogAppName);
                        if (processList.Length == 0)
                        {
                            Process.Start(watchdogExePath);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception WatchdogMonitor3: " + ex.StackTrace);
                }
                Thread.Sleep(watchDogMonitorInterval);
            }
        }

        /// <summary>
        /// This method uses a file create operation to notify operational status to the watchdog application
        /// </summary>
        private void StartHeartbeatThread()
        {
            while (true)
            {
                try
                {
                    File.Create(HEARTBEAT_FILE_NAME).Close();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception WatchdogMonitor4: " + ex.StackTrace);
                }
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Utility method to terminate the watchdog process
        /// </summary>
        public void KillWatchDog()
        {
            try
            {
                Process[] processList = Process.GetProcessesByName(watchdogAppName);
                if (processList.Length > 0)
                {
                    foreach (Process process in processList)
                    {
                        process.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception WatchdogMonitor5: " + ex.StackTrace);
            }
        }
    }
}