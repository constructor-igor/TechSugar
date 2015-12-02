using System;
using NUnit.Framework;

namespace NUnit_v2_samples
{
    public enum TimePeriodInputType { FromTo, YearsMonths }

    [TestFixture]
    public class NUnitIssue1033
    {
        //        [Datapoints]
        //        public Boolean[] bools = { true, false };
        //        [Datapoints]
        //        public TimePeriodInputType[] timePeriodInputTypes = { TimePeriodInputType.FromTo, TimePeriodInputType.YearsMonths };

        [Theory]
        public void ShouldGenerateForAnyTimePeriod(
            bool isCoApplicant,
            TimePeriodInputType timePeriodInputType,
            [Values(0, 2)] Int32 years,
            [Values(0, 5, 15)] Int32 months)
        {
            Console.WriteLine("isCoApplicant={0}, timePeriodInputType={1}", isCoApplicant, timePeriodInputType);
            Console.WriteLine("years={0}, months={1}", years, months);
        }
    }
}