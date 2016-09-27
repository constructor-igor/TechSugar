using ILNumerics;
using NUnit.Framework;

namespace ILNumericsTests
{
    [TestFixture]
    public class ILArraysTests
    {
        [Test]
        public void CreateILArray()
        {
            ILNumericsSample sample = new ILNumericsSample();
            sample.CreateArray();
        }
    }

    public class ILNumericsSample : ILMath
    {
        public void CreateArray()
        {
            ILArray<double> A = rand(10, 20);
            ILArray<double> B = A * 30 + 100;
            ILLogical C = any(multiply(B, B.T));
        }
    }
}
