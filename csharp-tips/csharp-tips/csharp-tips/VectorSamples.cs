using System;
using System.Linq;
using System.Numerics;
using BenchmarkIt;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace csharp_tips
{
    /*
     * http://habrahabr.ru/post/274605/
     * */
    [TestFixture]
    public class VectorSamples
    {
        [Test]
        public void Test()
        {
            //Console.WriteLine("Vector<char>.Count: {0}", Vector<Char>.Count);
            Console.WriteLine("Vector<Single>.Count: {0}", Vector<Single>.Count);
            Console.WriteLine("Vector<int>.Count: {0}", Vector<int>.Count);
            Console.WriteLine("Vector<double>.Count: {0}", Vector<double>.Count);
        }
        [Test]
        public void TestVectorsAdd()
        {
            const Int32 N = 8000;
            Fixture fixture = new Fixture();
            Single[] a = fixture.CreateMany<Single>(N).ToArray();
            Single[] b = fixture.CreateMany<Single>(N).ToArray();
//            Single[] a = { 41982.0F, 81.5091F, 3.14F, 42.666F, 54776.45F, 342.4556F, 6756.2344F, 4563.789F };
//            Single[] b = { 85989.111F, 156.5091F, 3.14F, 42.666F, 1006.45F, 9999.4546F, 0.2344F, 7893.789F };
            Single[] cVector = new Single[N];
            Single[] cStandard = new Single[N];

            SumVector(a, b, cVector);
            SumStandard(a, b, cStandard);
            Assert.That(cVector, Is.EquivalentTo(cStandard));

            Benchmark
                .This("standard", () => SumStandard(a, b, cStandard))
                .Against
                .This("Vector", () => SumVector(a, b, cVector))
                .WithWarmup(2)
                .For(1000000)
                .Iterations()
                .PrintComparison();
        }

        private static void SumVector(float[] a, float[] b, float[] c)
        {
            int N = c.Length;
            for (int i = 0; i < N; i += Vector<Single>.Count) // Count returns 16 for char, 4 for float, 2 for double.
            {
                var aSimd = new Vector<Single>(a, i); // create instance with offset i
                var bSimd = new Vector<Single>(b, i);
                Vector<Single> cSimd = aSimd + bSimd; // or Vector<Single> c_simd = Vector.Add(b_simd, a_simd);
                cSimd.CopyTo(c, i); //copy to array with offset
            }
        }
        private static void SumStandard(float[] a, float[] b, float[] c)
        {
            int N = c.Length;
            for (int i = 0; i < N; i ++)
            {
                c[i] = a[i] + b[i];
            }
        }
    }
}