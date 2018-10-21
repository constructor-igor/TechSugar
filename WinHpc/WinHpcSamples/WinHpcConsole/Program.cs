using System;
using System.Threading;
using Microsoft.Hpc.Scheduler;
using Microsoft.Hpc.Scheduler.Properties;

namespace WinHpcConsole
{
    class Program
    {
        private static readonly ManualResetEvent manualEvent = new ManualResetEvent(false);
        static void Main(string[] args)
        {
            Console.Write("hpc: ");
            string serverName = Console.ReadLine();
            Console.Write("user: ");
            string user = Console.ReadLine();
            Console.Write("job template (<Enter> - 'default'): ");
            string jobTemplate = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(jobTemplate))
                jobTemplate = "Default";

            IScheduler scheduler = new Scheduler();

            scheduler.Connect(serverName);
            ISchedulerJob job = scheduler.CreateJob();
            job.SetJobTemplate(jobTemplate);
            ISchedulerTask task = job.CreateTask();            
            task.CommandLine = "dir";
            job.AddTask(task);

            // Specify the events that you want to receive.
            job.OnJobState += JobStateCallback;
            job.OnTaskState += TaskStateCallback;

            // Start the job.
            scheduler.SubmitJob(job, user, null);
            manualEvent.WaitOne();
        }

        private static void TaskStateCallback(object sender, TaskStateEventArg e)
        {
        }

        private static void JobStateCallback(object sender, JobStateEventArg e)
        {
            string newState = e.NewState.ToString();
            Console.WriteLine($"{e.JobId}: {newState}");

            if (JobState.Canceled == e.NewState ||
                JobState.Failed == e.NewState ||
                JobState.Finished == e.NewState)
            {
                manualEvent.Set();
            }
        }
    }
}
