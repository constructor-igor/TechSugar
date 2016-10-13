using NUnit.Framework;

namespace csharp_tips
{
    public class ResharperSamples
    {
        public object MakeFunction()
        {
            return MakeFunction(10);
        }
        private object MakeFunction(int functionType)
        {
            return Helper.MakeFunction(functionType);
        }
    }
    public class Helper
    {
        public static object MakeFunction(int functionType)
        {
            return functionType > 0 ? new object() : -1;
        }
    }

    [TestFixture]
    public class ResharperSamplesTests
    {
        [Test]
        public void Test()
        {
            ResharperSamples samples = new ResharperSamples();
            var result = samples.MakeFunction();
            Assert.That(result, Is.Not.Null);
        }
    }
}