using System.Threading.Tasks;

namespace DailyPlannerService
{
    public class ItemProcessing
    {
        private readonly Task _task;
        private readonly ItemsStorage _itemsStorage;
        private readonly ItemsController _itemsController;
        private readonly CalendarContainer _calendarContainer;

        public ItemProcessing(ItemsStorage itemsStorage, ItemsController itemsController, CalendarContainer calendar)
        {
            _itemsStorage = itemsStorage;
            _itemsController = itemsController;
            _calendarContainer = calendar;
            _task = Task.Factory.StartNew(ProcessItem);
        }

        private void ProcessItem()
        {
            while (!_task.IsCanceled)
            {
                UserItem userItem = _itemsStorage.TakeItem();
                if (userItem != null)
                {
                    _calendarContainer.Add(userItem);
                }
                else
                {
                    _itemsController.Wait();
                }
            }
        }
    }
}