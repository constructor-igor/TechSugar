using ctor.location.framework;
using NUnit.Framework;

namespace ctor.location.tests
{
    [TestFixture]
    public class LocationTests
    {
        [Test]
        public void Sanity()
        {
            Location location = new Location();
            Assert.Pass();
        }
    }
}
