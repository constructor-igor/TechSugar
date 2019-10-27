using System;
using System.Collections.Generic;
using System.Linq;

namespace ChineseRemainderTheorem
{
    class Program
    {
        static void Main()
        {
            runChineseReminderTheorem(new List<int>{3, 5, 8}, new List<int>{2, 4, 7});
            runChineseReminderTheorem(new List<int> { 1003, 1005, 1008 }, new List<int> { 2, 4, 7 });
        }

        private static void runChineseReminderTheorem(List<int> vectorA, List<int> vectorR)
        {
            int N = FindN(vectorA, vectorR);
            Console.WriteLine($"N = {N}");
            for (int i = 0; i < vectorA.Count; i++)
            {
                Console.WriteLine(
                    $"a[{i}] = {vectorA[i]}, r[{i}]={vectorR[i]}, {N} = {vectorA[i]}*{N / vectorA[i]}+{vectorR[i]}");
            }
        }

        private static int FindN(List<int> vectorA, List<int> vectorR)
        {
            int candidateN = vectorA.Last();

            bool foundN = false;
            while (!foundN)
            {
                candidateN++;
                foundN = vectorA
                    .Select((value, index) => candidateN % value == vectorR[index])
                    .All(v=>v);
            }
            return candidateN;
        }
    }
}
