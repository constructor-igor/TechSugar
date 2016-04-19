using System;
using Quartz;

namespace QuartzClientConsole
{
    public class HelloJob : IJob
    {
        #region IJob
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("[{0}] Hello Job", DateTime.Now);
        }
        #endregion
    }
}