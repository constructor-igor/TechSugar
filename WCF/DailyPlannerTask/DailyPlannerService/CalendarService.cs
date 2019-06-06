using System.ServiceModel;

namespace DailyPlannerService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CalendarService : ICalendarService
    {
        private readonly CalendarContainer _calendarContainer;
        private readonly ItemProcessing _itemProcessing;
        private readonly ItemsStorage _itemsStorage;
        private readonly ItemsController _itemsController;

        public CalendarService()
        {
            _calendarContainer = new CalendarContainer();
            _itemsController = new ItemsController();
            _itemsStorage = new ItemsStorage(_itemsController);
            _itemProcessing = new ItemProcessing(_itemsStorage, _itemsController, _calendarContainer);
      }

        public Calendar GetData()
        {
            return _calendarContainer.GetCalendar();
        }

        public void SendData(UserItem userItem)
        {
            _itemsStorage.AddItem(userItem);
        }
    }
}
