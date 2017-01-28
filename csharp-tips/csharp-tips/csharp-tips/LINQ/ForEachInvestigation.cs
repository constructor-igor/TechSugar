using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace csharp_tips.LINQ
{
    /*
     * References:
     * - http://stackoverflow.com/questions/11179156/how-is-foreach-implemented-in-c
     * - http://stackoverflow.com/questions/398982/how-do-foreach-loops-work-in-c
     * 
     * */

    [TestFixture]
    public class ForEachInvestigation
    {
        [Test]
        public void ForEachForArray()
        {
            int[] dataArray = Enumerable.Range(0, 10).ToArray();
            foreach (int dataItem in dataArray)
            {
                Console.WriteLine(dataItem);
            }
        }
        [Test]
        public void ForEachForList()
        {
            List<int> dataList = Enumerable.Range(0, 10).ToList();
            foreach (int dataItem in dataList)
            {
                Console.WriteLine(dataItem);
            }
        }
        [Test]
        public void ForEachForEnumarable()
        {
            IEnumerable<int> dataEnumerable = Enumerable.Range(0, 10);
            foreach (int dataItem in dataEnumerable)
            {
                Console.WriteLine(dataItem);
            }
        }
    }
}