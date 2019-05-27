using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LogFileMonitoring
{
    class Program
    {
        private static void LinesToConsole(LogFileMonitorLineEventArgs msg)
        {
            foreach (string line in msg.Lines)
            {
                Console.WriteLine(line);
            }
        }
        static void Main()
        {
            string logFile = Path.GetTempFileName();
            Console.WriteLine($"Created log file {logFile}");
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            // Create a task and supply a user delegate by using a lambda expression. 
            Task taskLogWriter = new Task(() =>
            {
                Console.WriteLine("LogWriter started");
                int counter = 0;
                while (!token.IsCancellationRequested)
                {
                    string msg = $"{counter:0000} - {DateTime.Now.ToString(CultureInfo.InvariantCulture)}";
                    File.AppendAllLines(logFile, new[] {msg});
                    Thread.Sleep(1000);
                    counter++;
                    if (counter % 10 == 0)
                    {
                        Console.WriteLine($"Imported {counter} messages.");
                    }
                }
                Console.WriteLine("LogWriter finished");
            });
            Task taskLogReading = new Task(() =>
            {
                Console.WriteLine("LogReading started");
                using (new LogFileMonitor(logFile, LinesToConsole))
                {
                    while (!token.IsCancellationRequested)
                    {
                    }
                }
                Console.WriteLine("LogReading finished");
            });

            using (taskLogWriter)
            using (taskLogReading)
            {
                // Start the task.
                taskLogWriter.Start();
                taskLogReading.Start();

                Console.WriteLine("Press any key to stop...");
                Console.ReadKey();

                tokenSource.Cancel();
                taskLogWriter.Wait();
                taskLogReading.Wait();
            }
            Console.WriteLine($"Deleting log file {logFile}");
            File.Delete(logFile);
            Console.WriteLine($"Deleted log file {logFile}");
        }
    }
}
