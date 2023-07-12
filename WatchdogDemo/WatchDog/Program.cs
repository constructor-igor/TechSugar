using System;
using System.Diagnostics;
using System.Threading;

namespace WatchdogApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Watchdog Application");
            Console.WriteLine("Press any key to stop.");

            // Specify the path to the custom program you want to run
            string programPath = @"D:\ig\myprojects\WatchdogDemo\Binaries\\CustomProcess.exe";

            // Create a new thread to run the custom program
            Thread programThread = new Thread(() => RunProgram(programPath));
            programThread.Start();

            // Wait for user input to stop the watchdog
            Console.ReadKey();

            // Stop the program thread gracefully
            programThread.Abort();
        }

        static void RunProgram(string programPath)
        {
            string exeFolder = Environment.ProcessPath;
            Console.WriteLine($"Process path: {exeFolder}");
            Console.WriteLine($"BaseDirectory: {System.AppDomain.CurrentDomain.BaseDirectory}");
            Console.WriteLine($"GetCurrentDirectory: {Directory.GetCurrentDirectory()}");


            while (true)
            {
                try
                {
                    // Create a new process for the custom program
                    Process programProcess = new Process();
                    programProcess.StartInfo.FileName = programPath;

                    // Set the StartInfo properties to run the program in a separate console window
                    programProcess.StartInfo.UseShellExecute = true;
                    programProcess.StartInfo.CreateNoWindow = false;
                    programProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal;


                    // Start the program
                    programProcess.Start();

                    // Wait for the program to finish
                    programProcess.WaitForExit();

                    // Program has finished, restart it after a delay
                    Console.WriteLine("Program finished. Restarting in 5 seconds...");
                    Thread.Sleep(5000);
                }
                catch (ThreadAbortException)
                {
                    // ThreadAbortException will be thrown when we call thread.Abort()
                    // We can ignore it in this case since we just want to stop the thread gracefully
                }
                catch (Exception ex)
                {
                    // Handle any other exceptions that might occur during program execution
                    Console.WriteLine("An error occurred: " + ex.Message);
                    // Optionally, you can choose to break out of the loop or implement a retry mechanism here.
                    break;
                }
            }
        }
    }
}
