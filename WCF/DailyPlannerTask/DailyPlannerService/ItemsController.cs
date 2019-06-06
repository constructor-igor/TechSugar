using System.Threading;

namespace DailyPlannerService
{
    public class ItemsController
    {
        readonly AutoResetEvent _autoResetEvent = new AutoResetEvent(true);

        public void Set()
        {
            _autoResetEvent.Set();
        }

        public void Wait()
        {
            _autoResetEvent.WaitOne();
        }
    }
}