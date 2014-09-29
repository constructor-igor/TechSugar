using KellermanSoftware.CompareNetObjects;
using NUnit.Framework;

namespace DeepEqual_Samples.CompareNETObjects
{
    [TestFixture]
    public class CompareNetObjectsSimpleTypesSamples
    {
        [Test]
        [ExpectedException]
        public void IntTypeTesting()
        {
            AssertHelper.Wrapper(()=>
            {
                const int expected = 5;
                const int actual = 6;

                var compareLogic = new CompareLogic();
                ComparisonResult result = compareLogic.Compare(expected, actual);

                Assert.IsTrue(result.AreEqual, result.DifferencesString);
            });
        }

        [Test]
        [ExpectedException]
        public void CheckStringAssert()
        {
            AssertHelper.Wrapper(() =>
            {
                const string expected = "Joe";
                const string actual = "Chandler";

                var compareLogic = new CompareLogic();
                ComparisonResult result = compareLogic.Compare(expected, actual);

                Assert.IsTrue(result.AreEqual, result.DifferencesString);
            });
        }
    }
}
