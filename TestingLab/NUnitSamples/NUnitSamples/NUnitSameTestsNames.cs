using System;
using NUnit.Framework;

namespace NUnit_v2_samples
{
    [TestFixture]
    public class NUnitSameTestsNames
    {
        [Test]
        public void Test()
        {
            Console.WriteLine("Test");
        }

        [TestCase("a")]
        public void Test(string name)
        {
            Console.WriteLine("Test01");
        }
    }
}