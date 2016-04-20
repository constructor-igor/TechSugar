using System;
using Quartz;
using Quartz.Impl.Calendar;

namespace QuartzClientConsole.UserDefinedCalendarSample
{
    public class UserDefinedCalendar : ICalendar
    {
        public UserDefinedCalendar()
        {
            Description = "User Defined Calendar";
            CalendarBase = new BaseCalendar();
        }
        #region ICloneable
        public object Clone()
        {
            return new UserDefinedCalendar();
        }
        #endregion

        #region Implementation of ICalendar
        public bool IsTimeIncluded(DateTimeOffset timeUtc)
        {
            return timeUtc.Second%2==0;
        }
        public DateTimeOffset GetNextIncludedTimeUtc(DateTimeOffset timeUtc)
        {
            return new DateTimeOffset(timeUtc.DateTime, TimeSpan.FromSeconds(1));
        }

        public string Description { get; set; }
        public ICalendar CalendarBase { get; set; }
        #endregion
    }
}