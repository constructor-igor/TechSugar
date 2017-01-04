using System.Collections.Generic;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class ListSamples
    {
        [Test]
        public void Remove()
        {
            List<string> list = new List<string> {"first", "second", "third"};
            list.Remove("five");
            Assert.That(list.Count, Is.EqualTo(3));
        }
    }
}