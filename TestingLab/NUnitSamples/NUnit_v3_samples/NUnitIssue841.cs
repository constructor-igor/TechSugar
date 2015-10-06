using System;
using Moq;
using NUnit.Framework;

namespace NUnit_v3_samples
{
    public class Value
    {
        public double RowVale { get; set; }
    }
    [TestFixture]
    public class NUnitIssue841
    {
        private static Value[] Values = new Value[2];

        static NUnitIssue841()
        {
            Values[0] = new Value
            {
                RowVale = 10
            };
//            Values[1] = new Value
//            {
//                RowVale = 20
//            };

            var mockValue = new Mock<Value>();
            Values[1] = mockValue.Object;
        }

        [Test, TestCaseSource("Values")]
        public void TestTestCaseSourceWithMoq(Value value)
        {
            Console.WriteLine("RowVale: {0}", value.RowVale);
        }
    }
}