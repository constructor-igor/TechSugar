using System;
using System.Collections.Generic;
using DeepEqual;
using DeepEqual.Syntax;
using NUnit.Framework;

/*
 * Checking open source project https://github.com/jamesfoster/DeepEqual
 * 
 * */

namespace DeepEqual_Samples.DeepEqual
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
        public void CheckSeveralStrings()
        {
            var expected = new List<string> { "Joe", "data 10", "Sam1", "finish"};
            var actual = new List<string> { "Joe", "data 20", "Sam2", "finish"};

            actual.WithDeepEqual(expected)
                .Assert();
        }

        [Test]
        [ExpectedException]
        public void Check2LongStringAssert()
        {
            string string1;
            string string2;
            DataFactory.Prepare2LongDifferentStrings(out string1, out string2);

            AssertHelper.Wrapper(() => string2.WithDeepEqual(string1).Assert());
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

        [Test]
        [ExpectedException]
        public void DoubleTypeTestingWithReceivedExtensionMethod()
        {
            const double expected = 5.0;
            const double actual1 = 5.1;
            const double actual2 = 5.2;

            actual1.WithDeepEqual(expected)
                .WithFloatTolerance(0.15)
                .Assert();

            actual2.WithDeepEqual(expected)
                .WithFloatTolerance(0.15)
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

    public static class DeepEqualExtensions
    {
        public static CompareSyntax<T1, T2> WithFloatTolerance<T1, T2>(this CompareSyntax<T1, T2> syntax, double tolerance)
        {
            return syntax.WithCustomComparison(new MyComparison(tolerance));
        }
    }
}
