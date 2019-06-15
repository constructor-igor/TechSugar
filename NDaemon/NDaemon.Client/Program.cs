using System;
using NDaemon.Framework;

namespace NDaemon.Client
{
    class Program
    {
        private const string AppGuid = "9291DD5E-64C6-4C7F-9FBC-C0CB51F676C1";
        static void Main(string[] args)
        {
            Console.WriteLine($"[NDaemon.Client ({AppGuid})] started.");
            using (SingleProcess singleProcess = new SingleProcess(AppGuid))
            {
                if (singleProcess.OtherProcessRunning)
                {
                    Console.WriteLine($"[NDaemon.Client ({AppGuid})] found other process.");
                    return;
                }
                Console.WriteLine($"[NDaemon.Client ({AppGuid})] Single instance of the client started.");
                Console.Write("To exit from program, press <Enter>.");
                Console.ReadLine();
            }
            Console.WriteLine($"[NDaemon.Client ({AppGuid})] finished.");
        }
    }
}
