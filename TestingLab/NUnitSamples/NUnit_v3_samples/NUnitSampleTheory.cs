using System;
using NUnit.Framework;

namespace NUnit_v3_samples
{
    [TestFixture]
    public class TheorySampleTests_2X
    {
        [Datapoint]
        public double[,] Array2X2Double = new double[,] { { 1, 0 }, { 0, 1 } };
        [Datapoint]
        public int[,] Array2X2Int = new int[,] { { 1, 0 }, { 0, 1 } };

        [Theory]
        public void TestDoubleForArbitraryArray(double[,] array)
        {
            Console.WriteLine("TestDoubleForArbitraryArray()");
            TestGenericForArbitraryArray(array);
        }
        [Theory]
        public void TestIntForArbitraryArray(int[,] array)
        {
            Console.WriteLine("TestIntForArbitraryArray()");
            TestGenericForArbitraryArray(array);
        }

        //[Theory]
        private void TestGenericForArbitraryArray<T>(T[,] array)
        {
            Console.WriteLine("TestGenericForArbitraryArray()");
            // ...
        }
    }
    [TestFixture]
    public class TheorySampleTests
    {
        [Datapoint]
        public double[] ArrayDouble = { 0, 1, 2, 3 };
        [Datapoint]
        public int[] ArrayInt = { 0, 1, 2, 3 };

        [Theory]
        public void TestDoubleForArbitraryArray(double[] array)
        {
            Console.WriteLine("TestDoubleForArbitraryArray()");
            TestGenericForArbitraryArray(array);
        }
        [Theory]
        public void TestIntForArbitraryArray(int[] array)
        {
            Console.WriteLine("TestIntForArbitraryArray()");
            TestGenericForArbitraryArray(array);
        }

        //[Theory]
        private void TestGenericForArbitraryArray<T>(T[] array)
        {
            Console.WriteLine("TestGenericForArbitraryArray()");
        }
    }

    public class TheorySampleTestsBasic<T>
    {
        [Theory]
        public void TestGenericForArbitraryArray(T[] array)
        {
            Console.WriteLine("TestGenericForArbitraryArray()");
        }
    }
    [TestFixture]
    public class TheorySampleTestsChildDouble : TheorySampleTestsBasic<double>
    {
        [Datapoint]
        public double[] ArrayDouble1 = { 0, 1, 2, 3 };
        [Datapoint]
        public double[] ArrayDouble2 = { 4, 5, 6, 7 };
        [Datapoint]
        public int[] ArrayInt = { 0, 1, 2, 3 };
    }
    [TestFixture]
    public class TheorySampleTestsChildInt : TheorySampleTestsBasic<int>
    {
        [Datapoint]
        public int[] ArrayDouble1 = { 0, 1, 2, 3 };
        [Datapoint]
        public int[] ArrayDouble2 = { 4, 5, 6, 7 };
    }

    [TestFixture(typeof(int))]
    [TestFixture(typeof(double))]
    public class TheorySampleTestsGeneric<T>
    {
        [Datapoint]
        public double[] ArrayDouble1 = { 0, 1, 2, 3 };
        [Datapoint]
        public double[] ArrayDouble2 = { 4, 5, 6, 7 };
        [Datapoint]
        public int[] ArrayInt = { 0, 1, 2, 3 };
        [Theory]
        public void TestGenericForArbitraryArray(T[] array)
        {
            Console.WriteLine("TestGenericForArbitraryArray()");
        }
    }
}