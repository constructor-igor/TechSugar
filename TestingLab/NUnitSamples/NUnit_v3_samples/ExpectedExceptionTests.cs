using System;
using NUnit.Framework;
using NUnit_v3_samples.Infra;

namespace NUnit_v3_samples
{
    //
    // https://github.com/nunit/nunit-csharp-samples/blob/master/ExpectedExceptionExample/ExpectedExceptionAttribute.cs
    //
    [TestFixture]
    public class ExpectedExceptionTests
    {
        [Test]
        [ExpectedException(typeof(DivideByZeroException))]
        public void InvalidArgument_ExpectedException_DivideByZeroException()
        {
            double r = Div(2.0, 0.0);
        }

        double Div(double x, double y)
        {
            if (y == 0)
                throw new DivideByZeroException();
            return x/y;
        }
    }
}