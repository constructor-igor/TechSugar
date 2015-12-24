using System;

namespace DesignPatterns.Observer
{
    public class BillingService
    {
        public void Update(object subject)
        {
            if (subject is Album)
                GenerateCharge((Album)subject);
        }

        private void GenerateCharge(Album theAlbum)
        {
            Console.WriteLine("[BillingService] album.Name: {0}", theAlbum.Name);
        }
    }
}