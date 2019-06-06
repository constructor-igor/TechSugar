using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace DailyPlannerService
{
    [ServiceContract]
    public interface ICalendarService
    {
        [OperationContract]
        Calendar GetData();

        [OperationContract]
        void SendData(UserItem userItem);

    }

    [DataContract]
    public class Calendar
    {
        List<CalendarItem> items = new List<CalendarItem>();

        [DataMember]
        public List<CalendarItem> Items
        {
            get { return items; }
            set { items = value; }
        }
    }

    [DataContract]
    public class CalendarItem
    {
        List<string> items = new List<string>();

        [DataMember]
        public List<string> Items
        {
            get { return items; }
            set { items = value; }
        }
    }

    [DataContract]
    public class UserItem
    {
        private int startHourIndex = 0;
        private int endHourIndex = 0;
        private string userName = String.Empty;

        [DataMember]
        public int StartHourIndex
        {
            get { return startHourIndex; }
            set { startHourIndex = value; }
        }

        [DataMember]
        public int EndHourIndex
        {
            get { return endHourIndex; }
            set { endHourIndex = value; }
        }

        [DataMember]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
    }
}
