using Caliburn.Micro;
using DailyPlannerClient.CalendarService;

namespace DailyPlannerClient
{
    public class ShellViewModel : Conductor<Screen>
    {                
        readonly IWindowManager _windowManager;
        private readonly CalendarServiceClient _calendarServiceClient;

        private int _startHourIndex;
        public int StartHourIndex
        {
            get { return _startHourIndex; }
            set
            {
                _startHourIndex = value;
                NotifyOfPropertyChange();
            }
        }

        private int _endtHourIndex;
        public int EndHourIndex
        {
            get { return _endtHourIndex; }
            set
            {
                _endtHourIndex = value;
                NotifyOfPropertyChange();
            }
        }

        private StartHours _startHoursItems;
        public StartHours StartHoursItems
        {
            get { return _startHoursItems; }
            set
            {
                _startHoursItems = value;
                NotifyOfPropertyChange();
            }
        }

        private bool _enableAddUser;
        public bool EnableAddUser
        {
            get { return _enableAddUser; }
            set
            {
                _enableAddUser = value;
                NotifyOfPropertyChange();
            }
        }

        private EndHours _endHoursItems;
        public EndHours EndHoursItems
        {
            get { return _endHoursItems; }
            set
            {
                _endHoursItems = value;
                NotifyOfPropertyChange();
            }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                EnableAddUser = !string.IsNullOrEmpty(value);
                NotifyOfPropertyChange();
            }
        }

        public ShellViewModel()
        {
            DisplayName = "Sign";
            _startHoursItems = new StartHours();
            _endHoursItems = new EndHours();
            _windowManager = new WindowManager();
            _calendarServiceClient = new CalendarServiceClient();
            StartHourIndex = 0;
            EndHourIndex = 0;
            EnableAddUser = false;
        }

        public void AddUser()
        {
            UserItem userItem = new UserItem
            {
                UserName = _userName,
                StartHourIndex = _startHourIndex,
                EndHourIndex = _endtHourIndex
            };
            _calendarServiceClient.SendData(userItem);
            UserName = string.Empty;
        }

        public void ShowCalendar()
        {
            Calendar calendar = _calendarServiceClient.GetData();
            CalendarViewModel calendarViewModel = new CalendarViewModel(calendar, _startHoursItems, _endHoursItems);
            _windowManager.ShowWindow(calendarViewModel);
        }
    }
}
