using System;
using System.Collections.Generic;
using Quartz;
using Quartz.Impl;

namespace QuartzClientConsole.StateSample
{
    //
    // http://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/more-about-jobs.html
    //

    public class StateExecuter
    {
        public void StartSample()
        {
            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();

            // define the job and tie it to our HelloJob class
            JobDataMap newJobDataMap = new JobDataMap {{"myStateData", new List<DateTimeOffset>()}};

            IJobDetail job = JobBuilder.Create<StateJob>()
                .WithIdentity("myJob", "group1")
                .SetJobData(newJobDataMap)
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