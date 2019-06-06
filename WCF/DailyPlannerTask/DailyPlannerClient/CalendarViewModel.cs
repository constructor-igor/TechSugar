using System.Collections.ObjectModel;
using Caliburn.Micro;
using DailyPlannerClient.CalendarService;

namespace DailyPlannerClient
{
    public class CalendarViewModel : Screen
    {
        ObservableCollection<CalendarItemViewModel>  _calendarItems;

        public ObservableCollection<CalendarItemViewModel> CalendarItems
        {
            get { return _calendarItems; }
            set
            {
                _calendarItems = value;
                NotifyOfPropertyChange();
            }
        }

        public CalendarViewModel(Calendar calendar, StartHours startHours, EndHours endHours)
        {
            DisplayName = "Calendar";

            CalendarItems = new ObservableCollection<CalendarItemViewModel>();
            for (int i = 0; i < calendar.Items.Length; i++)
            {
                CalendarItem calendarItem = calendar.Items[i];
                CalendarItems.Add(new CalendarItemViewModel(calendarItem, startHours[i], endHours[i]));
            }
        }
    }
}
