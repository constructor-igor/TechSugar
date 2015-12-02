using System.Threading;
using NUnit.Framework;

namespace NUnit_v2_samples
{
    [TestFixture]
    public class NUnitIssue1111
    {
        [Test]
        [SetCulture("ru-RU")]
        public void NunitSetCultureTest()
        {
            Assert.That(Thread.CurrentThread.CurrentCulture.Name, Is.EqualTo("ru-RU"));
        }
    }
}