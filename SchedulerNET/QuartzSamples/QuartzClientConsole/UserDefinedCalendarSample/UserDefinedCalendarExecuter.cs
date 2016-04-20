using Quartz;
using Quartz.Impl;
using QuartzClientConsole.HelloWorldSample;

namespace QuartzClientConsole.UserDefinedCalendarSample
{
    //
    //  http://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/more-about-triggers.html
    //
    public class UserDefinedCalendarExecuter
    {
        public void StartSample()
        {
            ICalendar cal = new UserDefinedCalendar();
            
            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();
            sched.AddCalendar("userDefinedCalendar", cal, false, true);

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<HelloJob>()
                .WithIdentity("myJob", "group1")
                .Build();

            // Trigger the job to run now, and then every 5 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(1)
                    .RepeatForever())
                .ModifiedByCalendar("userDefinedCalendar") // but not on "user defined" calendar
                .Build();

            sched.ScheduleJob(job, trigger);
        }
    }
}