using System;
using Quartz;

namespace QuartzClientConsole.UserDefinedParametersSample
{
    public class UserDefinedParametersJob : IJob
    {
        #region IJob
        public void Execute(IJobExecutionContext context)
        {
            JobKey key = context.JobDetail.Key;
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string jobSays = dataMap.GetString("jobSays");
            float myFloatValue = dataMap.GetFloat("myFloatValue");
            Console.WriteLine("Instance '{0}' of DumbJob says: '{1}', and val is: '{2}'", key, jobSays, myFloatValue);
        }
        #endregion
    }
}