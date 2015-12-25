using NUnit.Framework;

namespace NUnit_v3_samples
{
    [TestFixture]
    public class NUnitIssue1141
    {
        [Test]
        [Explicit]
        public void ExplicitTestCase()
        {
            Assert.Fail("Should never be run, even not if using exclude for categories.");
        }
    }
}