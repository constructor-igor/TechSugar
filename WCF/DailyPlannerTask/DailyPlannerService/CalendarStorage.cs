using System.Collections.Generic;

namespace DailyPlannerService
{
    public class CalendarStorage
    {
        public List<CalendarItem> Items;

        public CalendarStorage()
        {
            Items = new List<CalendarItem>();
            for (int i = 0; i < 9; i++)
            {
                Items.Add(new CalendarItem());
            }
        }
        public void Add(UserItem userItem)
        {
            for (int i = userItem.StartHourIndex; i < userItem.EndHourIndex + 1; i++)
            {
                CalendarItem item = Items[i];
                if (!item.Items.Contains(userItem.UserName))
                {
                    item.Items.Add(userItem.UserName);
                }
            }
        }

    }
}