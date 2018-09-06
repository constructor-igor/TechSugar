using System;
using NUnit.Framework;

namespace csharp_tips.SOLID
{
    public interface IMath
    {
        double Calc(double x, double y);
    }

    public class Math : IMath
    {
        #region IMath
        public double Calc(double x, double y)
        {
            return x / y;
        }
        #endregion
    }
    public class ValidationMath: IMath
    {
        private readonly IMath m_math;

        public ValidationMath(IMath math)
        {
            m_math = math;
        }
        #region IMath
        public double Calc(double x, double y)
        {
            if (y==0)
                throw new ArgumentException();
            return m_math.Calc(x, y);
        }
        #endregion
    }

    [TestFixture]
    public class OpenClosedPrincipleTests
    {
        [Test]
        public void CalcWithCorrectParameters()
        {
            IMath math = new Math();
            double r = math.Calc(10, 2);
            Assert.That(r, Is.EqualTo(5));
        }
        [Test]
        public void CalcWithInCorrectParameters()
        {
            // we want to raise ArgumentException, when Calc cannot calculate 
            // the test fails, because Calc() returns Infinity, but we want ArgumentException
            IMath math = new Math();
            Assert.Throws<ArgumentException>(()=> math.Calc(10, 0));
        }
        [Test]
        public void CalcWithInCorrectParameters_ArgumentException()
        {
            // we want to raise ArgumentException, when Calc cannot calculate 
            // we add class ValidationMath and it allows
            //  to add new functionality (open principle)
            //  without changes in existing code (close principle)
            IMath actualMath = new Math();
            IMath math = new ValidationMath(actualMath);
            Assert.Throws<ArgumentException>(() => math.Calc(10, 0));
        }
    }
}