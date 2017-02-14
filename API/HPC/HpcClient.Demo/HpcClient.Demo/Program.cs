using System;
using Microsoft.Hpc.Scheduler;

namespace HpcClient.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] hpcNames = { "server1", "server2" };

            Scheduler scheduler = new Scheduler();
            foreach (string serverPath in hpcNames)
            {
                try
                {
                    scheduler.Connect(serverPath);
                    //Console.WriteLine("connected to {0}", serverPath);

                    ISchedulerCounters schedulerCounters = scheduler.GetCounters();
                    int runningJobs = schedulerCounters.RunningJobs;
                    Console.WriteLine("[{0}] running jobs (queue): {1}", serverPath, runningJobs);
                }
                catch (Exception e)
                {
                    Console.WriteLine("[{0}] Not accessible", serverPath);
                }
            }
       }
    }
}
