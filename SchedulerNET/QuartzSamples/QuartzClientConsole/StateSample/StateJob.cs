using System;
using System.Collections.Generic;
using Quartz;

namespace QuartzClientConsole.StateSample
{
    public class StateJob : IJob
    {
        #region IJob
        public void Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;
            JobDataMap dataMap = context.MergedJobDataMap;  // Note the difference from the previous example
            IList<DateTimeOffset> state = (IList<DateTimeOffset>)dataMap["myStateData"];           
            state.Add(DateTimeOffset.UtcNow);

            Console.WriteLine("[hashCode:{0}] state counter: {1}", GetHashCode(), state.Count);
        }
        #endregion
    }
}