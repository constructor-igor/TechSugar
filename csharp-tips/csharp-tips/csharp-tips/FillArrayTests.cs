using System;
using System.Linq;
using BenchmarkIt;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class FillArrayTests
    {
        private const int COUNT = 1000;
        private const int FILL_VALUE = 10;

        [Test]
        public void SanityTest()
        {
            int[] loopWay = CreateByStandardWay(COUNT, FILL_VALUE);
            int[] linqWay = CreateByLinqWay(COUNT, FILL_VALUE);
            int[] memSetHalfWay = CreateByMemSetHalfWay(COUNT, FILL_VALUE);
            int[] memSetX2Way = CreateByMemSetX2Way(COUNT, FILL_VALUE);
            Assert.That(linqWay, Is.EquivalentTo(loopWay));
            Assert.That(memSetHalfWay, Is.EquivalentTo(loopWay));
            Assert.That(memSetX2Way, Is.EquivalentTo(loopWay));
        }

        [Test]
        public void BenchMarkTest()
        {
            BenchmarkData benchmarkData = new BenchmarkData();
            benchmarkData.SetupData();

            BenchmarkIt.Benchmark.This("loop", () =>
            {
                int[] loopWay = CreateByStandardWay(COUNT, FILL_VALUE);
            })
            .Against.This("linq", () =>
            {
                int[] linqWay = CreateByLinqWay(COUNT, FILL_VALUE);
            })
            .Against.This("memset_half", () =>
            {
                int[] memSetHalfWay = CreateByMemSetHalfWay(COUNT, FILL_VALUE);
            })
            .Against.This("memset_X2", () =>
            {
                int[] memSetX2Way = CreateByMemSetX2Way(COUNT, FILL_VALUE);
            })
            .For(100000)
            .Iterations()
            .PrintComparison();
        }    
    
        private int[] CreateByLinqWay(int count, int value)
        {
            return Enumerable.Repeat(value, count).ToArray();
        }
        private int[] CreateByStandardWay(int count, int value)
        {
            int[] array = new int[count];
            for (int i = 0; i < array.Length; i++)
                array[i] = value;
            return array;
        }
        private int[] CreateByMemSetX2Way(int count, int value)
        {
            int[] array = new int[count];
            MemSetBlockX2(array, value);
            return array;
        }
        private int[] CreateByMemSetHalfWay(int count, int value)
        {
            int[] array = new int[count];
            Memset(array, value);
            return array;            
        }

        static void MemSetBlockX2(int[] array, int value)
        {
            int block = 32, index = 0, sizeOfInt = sizeof(int);
            int length = Math.Min(block, array.Length);

            //Fill the initial array
            while (index < length)
            {
                array[index++] = value;
            }

            length = array.Length;
            while (index < length)
            {
                int actualBlockSize = Math.Min(block, length - index);
                Buffer.BlockCopy(array, 0, array, index <<2, actualBlockSize <<2);
                index += block;
                block *= 2;
            }
        }
        public static void Memset<T>(T[] array, T elem)
        {
            int length = array.Length;
            array[0] = elem;
            int count;
            for (count = 1; count <= length / 2; count *= 2)
                Array.Copy(array, 0, array, count, count);
            Array.Copy(array, 0, array, count, length - count);
        }
    }
}