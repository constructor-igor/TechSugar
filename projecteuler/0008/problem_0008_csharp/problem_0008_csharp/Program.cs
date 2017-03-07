using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 * https://projecteuler.net/problem=8
 * 
 * */

namespace problem_0008_csharp
{    
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfDigits = 4;
            StringBuilder input = new StringBuilder()
                .Append("73167176531330624919225119674426574742355349194934")
                .Append("96983520312774506326239578318016984801869478851843")
                .Append("85861560789112949495459501737958331952853208805511")
                .Append("12540698747158523863050715693290963295227443043557")
                .Append("66896648950445244523161731856403098711121722383113")
                .Append("62229893423380308135336276614282806444486645238749")
                .Append("30358907296290491560440772390713810515859307960866")
                .Append("70172427121883998797908792274921901699720888093776")
                .Append("65727333001053367881220235421809751254540594752243")
                .Append("52584907711670556013604839586446706324415722155397")
                .Append("53697817977846174064955149290862569321978468622482")
                .Append("83972241375657056057490261407972968652414535100474")
                .Append("82166370484403199890008895243450658541227588666881")
                .Append("16427171479924442928230863465674813919123162824586")
                .Append("17866458359124566529476545682848912883142607690042")
                .Append("24219022671055626321111109370544217506941658960408")
                .Append("07198403850962455444362981230987879927244284909188")
                .Append("84580156166097919133875499200524063689912560717606")
                .Append("05886116467109405077541002256983155200055935729725")
                .Append("71636269561882670428252483600823257530420752963450");

            RunProblem(input, numberOfDigits);
            RunProblem(input, 13);
        }

        private static void RunProblem(StringBuilder input, int numberOfDigits)
        {
            byte zero = Convert.ToByte('0');
            List<byte> allDigits = input.ToString().Select(Convert.ToByte).Select(c => c - zero).Select(Convert.ToByte).ToList();

            UInt64 max = UInt64.MinValue;
            List<byte> foundDigits = new List<byte>();
            int foundIndex = -1;

            foreach (int index in Enumerable.Range(0, allDigits.Count - numberOfDigits + 1))
            {
                List<byte> currentOriginalDigits = allDigits.Skip(index).Take(numberOfDigits).ToList();
                IEnumerable<UInt64> currentDigits = currentOriginalDigits.Select(Convert.ToUInt64);
                UInt64 current = currentDigits.Aggregate((x, y) => x * y);
                if (current > max)
                {
                    max = current;
                    foundDigits = currentOriginalDigits;
                    foundIndex = index;
                }
            }
            Console.WriteLine("result for {0} digits: {1} in position {2} ({3})", numberOfDigits, max, foundIndex, String.Join(",", foundDigits.Select(f => f.ToString()).ToArray()));
        }
    }
}
