using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace csharp_tips.LINQ
{
    /*
     * 
     * sample from https://msdn.microsoft.com/library/bb534304(v=vs.100).aspx
     * 
     * */
    class Pet
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    [TestFixture]
    public class GroupBySamples
    {
        [Test]
        public void GroupByList_v1()
        {
            List<Pet> pets =
                    new List<Pet>{ new Pet { Name="Barley", Age=8 },
                                   new Pet { Name="Boots", Age=4 },
                                   new Pet { Name="Whiskers", Age=1 },
                                   new Pet { Name="Daisy", Age=4 } };

            IEnumerable<IGrouping<int, string>> query =
                    pets.GroupBy(pet => pet.Age, pet => pet.Name);

            foreach (IGrouping<int, string> petGroup in query)
            {
                // Print the key value of the IGrouping.
                Console.WriteLine(petGroup.Key);
                // Iterate over each value in the 
                // IGrouping and print the value.
                foreach (string name in petGroup)
                    Console.WriteLine("  {0}", name);
            }
        }
        [Test]
        public void GroupByList_v2()
        {
            List<Pet> pets =
                    new List<Pet>{ new Pet { Name="Barley", Age=8 },
                                   new Pet { Name="Boots", Age=4 },
                                   new Pet { Name="Whiskers", Age=1 },
                                   new Pet { Name="Daisy", Age=4 } };

            IEnumerable<IGrouping<int, string>> query =
                from pet in pets
                group pet.Name by pet.Age;

            foreach (IGrouping<int, string> petGroup in query)
            {
                // Print the key value of the IGrouping.
                Console.WriteLine(petGroup.Key);
                // Iterate over each value in the 
                // IGrouping and print the value.
                foreach (string name in petGroup)
                    Console.WriteLine("  {0}", name);
            }
        }
    }

    [TestFixture]
    public class ListToDictionarySample
    {
        class DataItem
        {
            public readonly int Key;
            public readonly double Value;

            public DataItem(int key, double value)
            {
                Key = key;
                Value = value;
            }
        }

        [Test]
        public void Test()
        {
            List<DataItem> list = new List<DataItem>
            {
                new DataItem(1, 10.0),
                new DataItem(1, 14.0),
                new DataItem(2, 20.0),
                new DataItem(4, 40.0),
                new DataItem(4, 41.0)
            };
            Dictionary<int, List<double>> expectedDictionary = new Dictionary<int, List<double>>
            {
                {1, new List<double> {10.0, 14.0}},
                {2, new List<double> {20.0}},
                {4, new List<double> {40.0, 41.0}}
            };

            Dictionary<int, List<double>> actualDictionary = list
                .GroupBy(dataItem => dataItem.Key, dataItem => dataItem.Value)
                .ToDictionary(item => item.Key, item => item.ToList());

            Assert.That(actualDictionary.Keys, Is.EquivalentTo(expectedDictionary.Keys));
            Assert.That(actualDictionary.Values, Is.EquivalentTo(expectedDictionary.Values));
        }
    }
}