using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class VarTests
    {
        [Test]
        public void Test()
        {
            List<Dictionary<string, int>> list1 = new List<Dictionary<string, int>>();
            var list2 = new List<Dictionary<string, int>>();
        }

        [Test]
        public void Test2()
        {
            List<int> list = new List<int> { 0, 1, 2 };

            var r = list.Select(item =>
                {
                    int item2 = item * 2;
                    return new {item, item2};
                })
                .ToList();

            foreach (var item in r)
            {
                Console.WriteLine("item: {0}", item);
            }

            int n = 10;
            double d = 20;
            var v = new {n, d};

            List<DataItem> someSource = new List<DataItem>();
            var result = from x in someSource where x.Name.Length > 3 select new {x.ID, x.Name};
        }

        public class DataItem
        {
            public int ID;
            public string Name;
        }
    }
}