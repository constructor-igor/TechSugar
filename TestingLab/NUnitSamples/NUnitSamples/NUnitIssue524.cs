using NUnit.Framework;

namespace NUnit_v2_samples
{
    [TestFixture]
    public class NUnitIssue524
    {
        [Test]
        public void AssertAreEqual()
        {
            char c = '\u0000';
            Assert.AreEqual(0, c);
        }
        [Test]
        public void AssertThat()
        {
            char c = '\u0000';
            Assert.That(c, Is.EqualTo(0));
        }
    }
}