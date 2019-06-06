using System.Collections.ObjectModel;

namespace DailyPlannerClient
{
    public class StartHours : ObservableCollection<string>
    {
        public StartHours()
        {
            Add("8:00");
            Add("9:00");
            Add("10:00");
            Add("11:00");
            Add("12:00");
            Add("13:00");
            Add("14:00");
            Add("15:00");
            Add("16:00");
        }
    }
}
