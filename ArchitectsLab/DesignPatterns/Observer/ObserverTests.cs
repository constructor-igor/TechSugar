using NUnit.Framework;

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
            BillingService billing = new BillingService();
            CounterService counter = new CounterService();
            Album album = new Album("Up");

            album.PlaySubject.AddObserver(billing);
            album.PlaySubject.AddObserver(counter);

            album.Play();
        }
    }
}