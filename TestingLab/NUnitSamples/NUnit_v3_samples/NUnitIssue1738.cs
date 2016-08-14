using System;
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
            Console.WriteLine("first");
            Assert.Pass();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void Test(int index)
        {
            if (index<2)
                Console.WriteLine("index: {0}", index);
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