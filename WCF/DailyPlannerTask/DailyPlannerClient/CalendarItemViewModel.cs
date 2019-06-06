using Caliburn.Micro;
using DailyPlannerClient.CalendarService;

namespace DailyPlannerClient
{
    public class CalendarItemViewModel : Screen
    {
        private string _startHour;
        public string StartHour
        {
            get { return _startHour; }
            set
            {
                _startHour = value;
                NotifyOfPropertyChange();
            }
        }

        private string _endHour;
        public string EndHour
        {
            get { return _endHour; }
            set
            {
                _endHour = value;
                NotifyOfPropertyChange();
            }
        }

        private string _userNames;
        public string UserNames
        {
            get { return _userNames; }
            set
            {
                _userNames = value;
                NotifyOfPropertyChange();
            }
        }

        private int _numberOfPeople;
        public int NumberOfPeople
        {
            get { return _numberOfPeople; }
            set
            {
                _numberOfPeople = value;
                NotifyOfPropertyChange();
            }
        }

        public CalendarItemViewModel(CalendarItem calendarItem, string startHour, string endHour)
        {
            UserNames = string.Join(", ", calendarItem.Items);
            StartHour = startHour;
            EndHour = endHour;
            NumberOfPeople = calendarItem.Items.Length;      
        }
    }
}
