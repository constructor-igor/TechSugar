using System;
using NUnit.Framework;

namespace NUnit_v2_samples._1056
{
    [TestFixture]
    public class NUnitIssue1056
    {
        [TestCaseSource(typeof(ItemTestCaseProvider), "TestCases")]
        public void TestItemQualityAdjustments(Item item)
        {
            Console.WriteLine("Name: {0}", item.Name);
            var program = new Program();
            program.AddItem(item);
            program.UpdateQuality();

            //return item.Quality;
        }

        [Test, TestCaseSource(typeof(SimpleItemTestCaseProvider), "TestCases")]
        public void SimpleTest(Item item)
        {
            Console.WriteLine("Name: {0}", item.Name);
        }
    }
}