using NUnit.Framework;

namespace NUnit_v3_samples._1056
{
    [TestFixture]
    public class NUnitIssue1056
    {
        [TestCaseSource(typeof(ItemTestCaseProvider))]
        public void TestItemQualityAdjustments(Item item)
        {
            var program = new Program();
            program.AddItem(item);
            program.UpdateQuality();

            //return item.Quality;
        }
    }
}