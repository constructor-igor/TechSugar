using System;
using System.Linq;
using NUnit.Framework;

namespace csharp_tips.LINQ
{
    [TestFixture]
    public class AggregateSamples
    {
        [Test]
        public void AggregateInts()
        {
            int[] array = { 1, 2, 3, 4, 5 };
            var counter = 0;
            int result = array.Aggregate((a, b) =>
            {
                counter++;
                return a + b;
            });
            Assert.That(result, Is.EqualTo(15));
            Assert.That(counter, Is.EqualTo(4));

            int[] array1 = {1};
            counter = 0;
            int result1 = array1.Aggregate((a, b) =>
            {
                counter++;
                return a + b;
            });
            Assert.That(result1, Is.EqualTo(1));
            Assert.That(counter, Is.EqualTo(0));

            int[] array0 = {};
            Assert.That(() => array0.Aggregate((a, b)=>a+b), Throws.Exception.TypeOf<InvalidOperationException>());

            result = array.Aggregate(100, (a, b) => a + b);
            Assert.That(result, Is.EqualTo(115));

            result = array0.Aggregate(100, (a, b) => a + b);
            Assert.That(result, Is.EqualTo(100));
        }
    }
}
