using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class PowerShellInDebuggerSamples
    {
        [Test]
        public void SanityTest()
        {
            bool boolVariable = true;
            int intVariable = 10;
            double[] doubleArray = {0.0, 1.0, 2.0};

            Assert.That(doubleArray, Is.Not.Null);
        }
    }
}