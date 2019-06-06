namespace DailyPlannerService
{
    public class CalendarContainer
    {
        private readonly CalendarStorage _editableCalendar;

        public CalendarContainer()
        {
            _editableCalendar = new CalendarStorage();
        }
        public void Add(UserItem userItem)
        {
            _editableCalendar.Add(userItem);
        }

        public Calendar GetCalendar()
        {
            Calendar calendar = new Calendar();
            foreach (var item in _editableCalendar.Items)
            {
                calendar.Items.Add(item);
            }
            return calendar;
        }
    }
}