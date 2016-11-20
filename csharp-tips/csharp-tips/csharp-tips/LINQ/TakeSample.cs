using System.Linq;
using NUnit.Framework;

namespace csharp_tips.LINQ
{
    [TestFixture]
    public class TakeSample
    {
        [Test]
        public void TestEnoughData()
        {
            int[] data = Enumerable.Range(0, 10).ToArray();
            int[] part = data.Take(5).ToArray();
            Assert.That(part.Length, Is.EqualTo(5));
        }
        [Test]
        public void TestNotEnoughData()
        {
            int[] data = Enumerable.Range(0, 10).ToArray();
            int[] part = data.Take(15).ToArray();
            Assert.That(part.Length, Is.EqualTo(10));
        }
    }
}