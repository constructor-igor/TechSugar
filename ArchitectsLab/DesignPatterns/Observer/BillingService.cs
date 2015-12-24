using System;

namespace DesignPatterns.Observer
{
    /*
     * 
     * Weak Reference: http://www.dotnetperls.com/weakreference
     * 
     * 
     * */

    public class BillingService
    {
        public BillingService()
        {
            Console.WriteLine("BillingService()");
        }
        ~BillingService()
        {
            Console.WriteLine("~BillingService()");
        }

        public WeakReference SomeObject { get; set; }

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