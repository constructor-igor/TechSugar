using System.Threading;
using NUnit.Framework;

namespace NUnit_v3_samples
{
    [TestFixture]
    [SetCulture("ru-RU")]
    class NUnitIssue1111
    {
        [Test]        
        public void NunitSetCultureTest()
        {
            Assert.That(Thread.CurrentThread.CurrentCulture.Name, Is.EqualTo("ru-RU"));
        }
    }
}