using System.Collections.Generic;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class CombinatorialSamples
    {
        [Test]
        public void N_4_K_1_Expected_4()
        {
            int[] data = { 2, 3, 6, 1 };
            int n = data.Length;
            int k = 1;
            Combinations combinations = new Combinations();
            List<List<int>> combination = combinations.Generate(data, 0, k);

            Assert.That(combination.Count, Is.EqualTo(Fact(n) / (Fact(k) * Fact(n - k))));
            Assert.That(combination[0], Is.EqualTo(new List<int>{2}));
            Assert.That(combination[3], Is.EqualTo(new List<int>{1}));
        }
        [Test]
        public void N_4_K_2_Expected_6()
        {
            int[] data = { 2, 3, 6, 1 };
            int n = data.Length;
            int k = 2;
            Combinations combinations = new Combinations();
            List<List<int>> combination = combinations.Generate(data, 0, k);
            
            Assert.That(combination.Count, Is.EqualTo(Fact(n)/(Fact(k)*Fact(n-k))));
            Assert.That(combination[0], Is.EqualTo(new List<int> { data[0], data[1] }));
            Assert.That(combination[5], Is.EqualTo(new List<int> { data[2], data[3] }));
        }
        [Test]
        public void N_4_K_3_Expected_4()
        {
            int[] data = { 2, 3, 6, 1 };
            int n = data.Length;
            int k = 3;
            Combinations combinations = new Combinations();
            List<List<int>> combination = combinations.Generate(data, 0, k);

            Assert.That(combination.Count, Is.EqualTo(Fact(n) / (Fact(k) * Fact(n - k))));
            Assert.That(combination[0], Is.EqualTo(new List<int> { data[0], data[1], data[2] }));
            Assert.That(combination[3], Is.EqualTo(new List<int> { data[1], data[2], data[3] }));
        }

        int Fact(int n)
        {
            if (n == 1)
                return 1;
            return n*Fact(n - 1);
        }
    }

    public class Combinations
    {
        public List<List<int>> Generate(int[] data, int startingIndex, int combinationsSize)
        {
            List<List<int>> combinations = new List<List<int>>();

            if (combinationsSize == 0)
                return combinations;
            if (combinationsSize == 1)
            {
                for (int arrayIndex = startingIndex; arrayIndex < data.Length; arrayIndex++)
                {
                    combinations.Add(new List<int> {data[arrayIndex]});
                }
                return combinations;
            }
//            if (combinationsSize == 2)
//            {
//                int combinationListsIndex = 0;
//                for (int arrayIndex = startingIndex; arrayIndex < data.Length; arrayIndex++)
//                {
//                    for (int i = arrayIndex + 1; i < data.Length; i++)
//                    {
//                        combinations.Add(new List<int>());
//                        combinations[combinationListsIndex].Add(data[arrayIndex]);
//                        while (combinations[combinationListsIndex].Count < combinationsSize)
//                        {
//                            combinations[combinationListsIndex].Add(data[i]);
//                        }
//                        combinationListsIndex++;
//                    }
//                }
//                return combinations;
//            }

            for (int i = startingIndex; i < data.Length - combinationsSize + 1; i++)
            {
                List<List<int>> partialCombinations = Generate(data, i + 1, combinationsSize - 1);
                for (int index = 0; index < partialCombinations.Count; index++)
                {
                    partialCombinations[index].Insert(0, data[i]);
                    combinations.Add(partialCombinations[index]);
                }
//                for (int k = 0; k < partialCombinations.Count; k++)
//                {
//                    combinations.Add(partialCombinations[k]);
//                }
            }

            return combinations;
        }
    }
}