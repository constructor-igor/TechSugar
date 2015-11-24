using System;
using NUnit.Framework;

namespace NUnit_v3_samples
{
    [TestFixture]
    public class NUnitRepeatTests
    {
        private static int counter = 0;

        [Test]
        [Repeat(4)]
        public void SimpleTest()
        {
            Console.WriteLine(counter++);
        }

        [TestCase("testCase")]
        [Repeat(4)]
        public void CaseTest(string testName)
        {
            Console.WriteLine("{0}, {1}", testName, counter++);
        }
    }
}