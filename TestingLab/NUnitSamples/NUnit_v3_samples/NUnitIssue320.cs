using System;
using NUnit.Framework;

namespace NUnit_v3_samples
{
    [TestFixture]
    public class NUnitIssue320_v1
    {
        [Test]
        public void Test()
        {

        }
    }

    [TestFixture("MyParam")]
    [TestFixture("MyParam1")]
    public class NUnitIssue320
    {
        public NUnitIssue320(string testFixtureName)
        {
        }
        public object MySource = new[] { "Test1", "Test2" };

        [TestCaseSource("MySource")]
        public void MyTest(string source)
        {
            Assert.That(source, Is.EqualTo("Test2"));
        }

        [Test]
        public void Test()
        {
            Assert.Pass();
        }
    }
}