using System;
using System.Diagnostics;
using System.Threading;

namespace TimeDisplayApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Time Display Application");
            Console.WriteLine("Press any key to stop.");

            // Start a new thread to display time every 10 seconds
            Thread timeDisplayThread = new Thread(DisplayTime);
            timeDisplayThread.Start();

            // Wait for user input to stop the program
            Console.ReadKey();

            // Stop the time display thread gracefully
            timeDisplayThread.Abort();
        }

        static void DisplayTime()
        {
            Process currentProcess = Process.GetCurrentProcess();
            try
            {
                while (true)
                {
                    // Get the current time and display it
                    DateTime currentTime = DateTime.Now;
                    Console.WriteLine($"Process: {currentProcess.ProcessName}({currentProcess.Id}), Current Time: " + currentTime.ToString("HH:mm:ss"));

                    // Pause the thread for 5 seconds
                    Thread.Sleep(5000);
                }
            }
            catch (ThreadAbortException)
            {
                // ThreadAbortException will be thrown when we call thread.Abort()
                // We can ignore it in this case since we just want to stop the thread gracefully
            }
        }
    }
}
