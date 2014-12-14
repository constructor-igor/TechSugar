using NUnit.Framework;
using SimpleMathSample;

namespace Tests
{
    [TestFixture]
    public class CustomMathTests
    {
        [Test]
        public void Test()
        {
            var customMath = new CustomMath();
            Assert.AreEqual(30, customMath.Calc(10, 20));
        }
    }
}
