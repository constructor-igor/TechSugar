using KellermanSoftware.CompareNetObjects;
using NUnit.Framework;

namespace DeepEqual_Samples.CompareNETObjects
{
    [TestFixture]
    class CompareNetObjectsSimpleTypesSamples
    {
        [Test]
        [ExpectedException]
        public void IntTypeTesting()
        {
            Helper.Wrapper(()=>
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
            Helper.Wrapper(() =>
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
