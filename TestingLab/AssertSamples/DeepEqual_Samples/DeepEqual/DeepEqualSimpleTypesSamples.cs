using System;
using DeepEqual;
using DeepEqual.Syntax;
using NUnit.Framework;

/*
 * Checking open source project https://github.com/jamesfoster/DeepEqual
 * 
 * */

namespace DeepEqual_Samples
{
    [TestFixture]
    public class DeepEqualSimpleTypesSamples
    {
        [Test]
        [ExpectedException]
        public void IntTypeTesting()
        {
            const int expected = 5;
            const int actual = 6;

            actual.WithDeepEqual(expected)
                .Assert();
        }
        [Test]
        [ExpectedException]
        public void IntTypeTesting_ShouldDeepEqual()
        {
            const int expected = 5;
            const int actual = 6;

            actual.ShouldDeepEqual(expected);
        }

        [Test]
        [ExpectedException]
        public void DoubleTypeTesting()
        {
            const double expected = 5.0;
            const double actual1 = 5.1;
            const double actual2 = 5.2;

            IComparison comparison = new MyComparison(0.15);

            actual1.WithDeepEqual(expected)
                .WithCustomComparison(comparison)
                .Assert();

            actual2.WithDeepEqual(expected)
                .WithCustomComparison(comparison)
                .Assert();
        }
    }

    public class MyComparison : IComparison
    {
        private readonly double m_tolerance;

        public MyComparison(double tolerance)
        {
            m_tolerance = tolerance;
        }

        public bool CanCompare(Type type1, Type type2)
        {
            return (type1 == typeof (double) && type2==typeof(double));
        }

        public ComparisonResult Compare(IComparisonContext context, object value1, object value2)
        {
            var x = (double) value1;
            var y = (double) value2;

            if (Math.Abs(x-y) < m_tolerance)
                return ComparisonResult.Pass;

            return ComparisonResult.Fail;
        }
    }
}
