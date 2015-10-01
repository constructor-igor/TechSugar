using NUnit.Framework;

namespace NUnit_v3_samples
{
    [TestFixture]
    [Ignore("ignore")]
    [Explicit, Category("SomeCategory")]
    public class ExplicitTests
    {
        [Test]
        public void Test1()
        {
        }
        [Test]
        public void Test2()
        {
        }
    }
}