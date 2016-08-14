using System.Threading;
using NUnit.Framework;

namespace NUnit_v3_samples
{
    [TestFixture]
    public class NUnitIssue1738
    {
        [Test]
        public void Test_First()
        {
            Assert.Pass();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        public void Test(int index)
        {
            Thread.Sleep(2000);
            if (index%2==0)
                Assert.Pass("test #{0:00}", index);
            else
                Assert.Fail("test #{0:00}", index);
        }

        [Test]
        public void Test_Last()
        {
            Assert.Pass();
        }
    }
}