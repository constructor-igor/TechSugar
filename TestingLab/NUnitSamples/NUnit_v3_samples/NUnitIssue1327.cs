using NUnit.Framework;

namespace NUnit_v3_samples
{
    [TestFixture]
    public class NUnitIssue1327
    {
        [TestCaseSource(typeof(TestData), "NullArgs")]
        public void NullArgs(string[] args)
        {
            Assert.Pass();
        }
        [TestCaseSource(typeof(TestData), "SingleArg")]
        public void SingleArg(string[] args)
        {
            Assert.Pass();
        }
        [TestCaseSource(typeof(TestData), "TwoArgs")]
        public void TwoArgs(string[] args)
        {
            Assert.Pass();
        }
        [TestCaseSource(typeof(TestData), "WrongArgs")]
        public void WrongArgs(string[] args)
        {
            Assert.Pass();
        }
        [TestCaseSource(typeof(TestData), "WorkaroundArg")]
        public void WorkaroundArg(string[] args)
        {
            Assert.Pass();
        }
    }

    public class TestData
    {
        public static readonly string[][] WrongArgs = { null, new string[0], new[] { "1" }, };
        public static readonly string[][] NullArgs = null;
        public static readonly string[][] SingleArg = { new[] { "1" } };
        public static readonly string[][] TwoArgs = { new[] { "1", "2" } };
        public static readonly string[][] WorkaroundArg = {new[] {"1"}};
    }
}