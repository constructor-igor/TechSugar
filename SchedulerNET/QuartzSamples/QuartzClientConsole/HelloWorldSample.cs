using Quartz;
using Quartz.Impl;

namespace QuartzClientConsole
{
    //
    //  http://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/using-quartz.html
    //
    public class HelloWorldSample
    {
        public void StartSample()
        {
            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            // Trigger the job to run now, and then every 5 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(5)
                    .RepeatForever())
                .Build();

            sched.ScheduleJob(job, trigger);
        }        
    }
}