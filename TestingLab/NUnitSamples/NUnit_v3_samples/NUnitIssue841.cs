using System;
using Moq;
using NUnit.Framework;

namespace NUnit_v3_samples
{
    public interface IValue
    {
        double RowVale { get; set; }
    }
    public class Value: IValue
    {
        public double RowVale { get; set; }
    }
    [TestFixture]
    public class NUnitIssue841
    {
        private static IValue[] Values = new IValue[2];

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

            var mockValue = new Mock<IValue>();
            mockValue.Setup(foo => foo.RowVale).Returns(30.0);
            Values[1] = mockValue.Object;
        }

        [Test, TestCaseSource("Values")]
        public void TestTestCaseSourceWithMoq(IValue value)
        {
            Console.WriteLine("RowVale: {0}", value.RowVale);
        }
    }

    [TestFixture]
    public class NUnitIssue841_Moq
    {
        [Test]
        public void TestMoq()
        {
            var mockValue = new Mock<IValue>();
            mockValue.Setup(foo => foo.RowVale).Returns(30.0);
            Assert.That(mockValue.Object.RowVale, Is.EqualTo(30.0));
        }
    }
}