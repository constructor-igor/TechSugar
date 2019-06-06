using System.Collections.Concurrent;

namespace DailyPlannerService
{
    public class ItemsStorage
    {
        private readonly ConcurrentQueue<UserItem> _concurrentQueue;
        private readonly ItemsController _itemsController;

        public ItemsStorage(ItemsController messageController)
        {
            _concurrentQueue = new ConcurrentQueue<UserItem>();
            _itemsController = messageController;
        }

        public void AddItem(UserItem userItem)
        {
            _concurrentQueue.Enqueue(userItem);
            _itemsController.Set();
        }

        public UserItem TakeItem()
        {
            UserItem currentItem;
            return _concurrentQueue.TryDequeue(out currentItem) ? currentItem : null;
        }

        public bool IsEmpty()
        {
            return _concurrentQueue.Count <= 0;
        }
    }
}