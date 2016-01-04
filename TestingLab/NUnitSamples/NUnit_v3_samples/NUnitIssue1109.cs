using System;
using NUnit.Framework;

namespace NUnit_v3_samples
{
    [TestFixture]
    public class NUnitIssue1109: BaseFixture1109
    {
        [Test]
        public void SampleTestMethod([ValueSource(/*typeof(BaseFixture1109),*/ "MyDataSource")] int value)
        {
            Console.WriteLine("Value: " + value);
        }
    }

    public class BaseFixture1109
    {
        public static int[] MyDataSource = { 1, 2, 3 };
    }
}