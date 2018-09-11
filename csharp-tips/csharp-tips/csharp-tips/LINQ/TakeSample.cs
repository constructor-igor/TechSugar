using System;
using System.Collections.Generic;
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

        [Test]
        public void TestSelect()
        {
            Console.WriteLine("start");
            List<int> data = new List<int>{0, 2, 4, 5};
            var query = data.Select(item =>
            {
                Console.WriteLine($"item: {item}");
                return item * 2;
            });
            Console.WriteLine("after select");
            data.Add(10);
            int[] result = query.ToArray();
            Console.WriteLine(query.Count());
            Console.WriteLine($"result: {string.Join(",", result)}");
            Console.WriteLine("finish");
        }
    }
}