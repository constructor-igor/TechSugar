using System;

namespace DesignPatterns.Observer
{
    public class CounterService
    {
        public void Update(object subject)
        {
            Console.WriteLine("[CounterService] album.Name: {0}", ((Album)subject).Name);
        }
    }
}