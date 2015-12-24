using NUnit.Framework;
using System;
using System.Threading;

namespace DesignPatterns.Observer
{
    /*
     * 
     * https://github.com/constructor-igor/TechSugar/issues/31
     * https://msdn.microsoft.com/en-us/library/ff648108.aspx
     * 
     * */

    [TestFixture]
    public class ObserverTests
    {
        [Test]
        public void Client()
        {
            RunTest(true, new BillingService());

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Thread.Sleep(5000);
        }

        public void RunTest(bool subscribe, BillingService billing)
        {
            Album album = new Album("Up");

            if (subscribe)
            {
                billing.SomeObject = new WeakReference(album);
                album.PlayEvent += billing.Update;
            }
            album.Play();
        }
    }
}