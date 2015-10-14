using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NUnit_v2_samples
{
    [TestFixture]
    public class NUnitSimpleTypesTests
    {
        [Test]
        public void IntVsDouble()
        {
            int actual = 1;
            double expected = 1.0;

            Assert.That(actual, Is.EqualTo(expected));
        }
        [Test]
        public void ListIntVsDouble()
        {
            List<int>actual = new List<int>{1, 2};
            List<double> expected = new List<double>{1.0, 2.0};

            Assert.That(actual, Is.EquivalentTo(expected));
        }
        [Test]
        public void TupleIntVsDouble()
        {
            Tuple<int, int> actual = Tuple.Create(1, 2);
            Tuple<double, double> expected = Tuple.Create(1.0, 2.0 );

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}