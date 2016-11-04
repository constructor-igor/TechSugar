using System;
using NUnit.Framework;

namespace NUnit_v3_samples
{
    [TestFixture]
    public class RepeatSampleTests
    {
        readonly Random m_random = new Random();
        [Test]
        [Repeat(10)]
        public void RepeatTest()
        {
            int actual = m_random.Next(0, 10);
            Console.WriteLine("actual={0}", actual);
            Assert.That(actual, Is.LessThan(6));
        }
        [Test]
        [Retry(10)]
        public void RetryTest()
        {
            int actual = m_random.Next(0, 10);
            Console.WriteLine("actual={0}", actual);
            Assert.That(actual, Is.LessThan(6));
        }

        [Test, Repeat(100)]
        public void SearchFailedInputInProductionCode()
        {
            int inputValue = TestContext.CurrentContext.Random.Next(0, 10);
            RunProductionCode(inputValue);
            Assert.Pass();
        }

        void RunProductionCode(int inputValue)
        {
            if (inputValue > 8) throw new NotImplementedException();
        }
    }
}