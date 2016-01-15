using System;
using System.Numerics;
using NUnit.Framework;

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
            const Int32 N = 8;
            Single[] a = { 41982.0F, 81.5091F, 3.14F, 42.666F, 54776.45F, 342.4556F, 6756.2344F, 4563.789F };
            Single[] b = { 85989.111F, 156.5091F, 3.14F, 42.666F, 1006.45F, 9999.4546F, 0.2344F, 7893.789F };
            Single[] c = new Single[N];

            for (int i = 0; i < N; i += Vector<Single>.Count) // Count returns 16 for char, 4 for float, 2 for double.
            {
                var aSimd = new Vector<Single>(a, i); // create instance with offset i
                var bSimd = new Vector<Single>(b, i);
                Vector<Single> cSimd = aSimd + bSimd; // or Vector<Single> c_simd = Vector.Add(b_simd, a_simd);
                cSimd.CopyTo(c, i); //copy to array with offset
            }

            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine(c[i]);
            }
        }
    }
}