using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace LogFileMonitoring
{
    class Program
    {
        static void Main(string[] args)
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
                    File.AppendAllLines(logFile, new string[] {msg});
                    Thread.Sleep(1000);
                    counter++;
                    if (counter % 10 == 0)
                    {
                        Console.WriteLine($"Imported {counter} messages.");
                    }
                }
                Console.WriteLine("LogWriter finished");
            });
            using (taskLogWriter)
            {
                // Start the task.
                taskLogWriter.Start();

                Console.WriteLine("Press any key to stop...");
                Console.ReadKey();

                tokenSource.Cancel();
                taskLogWriter.Wait();
            }
            Console.WriteLine($"Deleting log file {logFile}");
            File.Delete(logFile);
            Console.WriteLine($"Deleted log file {logFile}");
        }
    }
}
