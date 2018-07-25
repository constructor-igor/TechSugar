using ctor.location.framework;
using NUnit.Framework;

namespace ctor.location.tests
{
    [TestFixture]
    public class SplitterTests
    {
        [Test]
        public void Simple()
        {
            Assert.That(SplitterHelper.Splitter("a,b,c,d"), Is.EqualTo(new[] {"a", "b", "c", "d"}));
        }
        [Test]
        public void WithSpace()
        {
            Assert.That(SplitterHelper.Splitter("a,b c,d"), Is.EqualTo(new[] { "a", "b c", "d" }));
        }
        [Test]
        public void WithQuotes()
        {
            Assert.That(SplitterHelper.Splitter("a,b,\"c,d\""), Is.EqualTo(new[] { "a", "b", "\"c,d\"" }));
        }
    }
}