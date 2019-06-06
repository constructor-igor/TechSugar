using System.Collections.ObjectModel;

namespace DailyPlannerClient
{
    public class EndHours : ObservableCollection<string>
    {
        public EndHours()
        {
            Add("9:00");
            Add("10:00");
            Add("11:00");
            Add("12:00");
            Add("13:00");
            Add("14:00");
            Add("15:00");
            Add("16:00");
            Add("17:00");
        }
    }
}
