using System;
using Quartz;

namespace QuartzClientConsole.UserDefinedCalendarSample
{
    public class CalendarJob : IJob
    {
        #region IJob
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("[{0}, HashCode = {1}] User Defined Calendar Job", DateTime.Now, GetHashCode());
        }
        #endregion
    }
}
