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
            BillingService service = new BillingService();
            Album album = new Album(service, "Up");

            album.Play();
        }
    }
}