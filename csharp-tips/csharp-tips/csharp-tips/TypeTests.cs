using System;
using BenchmarkIt;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class TypeTests
    {
        [Test]
        public void Test()
        {
            bool result1 = TestGeneric1(10.0f);
            bool result2 = TestGeneric2(10.0f);

            Type typeFloat = typeof(float);
            if (typeFloat != typeof(float))
                Assert.Fail();
            Assert.Pass();
        }

        [Test]
        public void BenchMarkTest()
        {
            BenchmarkData benchmarkData = new BenchmarkData();
            benchmarkData.SetupData();

            BenchmarkIt.Benchmark.This("TestGeneric1", () =>
            {
                bool r1 = TestGeneric1(10.0f);
            })
            .Against.This("TestGeneric2", () =>
            {
                bool r2 = TestGeneric2(10.0f);
            })
            .WithWarmup(10)
            .For(10000000)
            .Iterations()
            .PrintComparison();
        }    


        bool TestGeneric1<T>(T value)
        {
            Type actualType = typeof (T);
            Type floatType = typeof (float);

            bool r1 = actualType == floatType;
            bool r2 = actualType == floatType;
            bool r3 = actualType == floatType;
            return r1 && r2 && r3;
        }
        bool TestGeneric2<T>(T value)
        {
            bool r1 = typeof(T) == typeof(float);
            bool r2 = typeof(T) == typeof(float);
            bool r3 = typeof(T) == typeof(float);
            return r1 && r2 && r3;
        }
    }
}