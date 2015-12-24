using System;

namespace DesignPatterns.Observer
{
    public class BillingService : IObserver
    {
        public void Update(object subject)
        {
            Album album = subject as Album;
            if (album != null)
                GenerateCharge(album);
        }

        private void GenerateCharge(Album album)
        {
            string name = album.Name;
            Console.WriteLine("[BillingService] album.Name: {0}", name);
            //code to generate charge for correct album
        }
    }

    public class CounterService : IObserver
    {
        #region Implementation of IObserver
        public void Update(object subject)
        {
            Console.WriteLine("[CounterService] album.Name: {0}", ((Album)subject).Name);
        }
        #endregion
    }
}