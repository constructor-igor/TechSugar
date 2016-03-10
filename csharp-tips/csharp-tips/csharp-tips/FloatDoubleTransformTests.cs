using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkIt;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace csharp_tips
{
    [TestFixture]
    public class FloatDoubleTransformTests
    {
        [Test]
        public void SanityTest()
        {
            Fixture fixture = new Fixture();
            float[] floatData = fixture.CreateMany<float>(100).ToArray();
            double[] doubleDataByStandardWay = StandardWay.CreateDoubleArray(floatData);
            double[] doubleDataByFastWay = FastWay.CreateDoubleArray(floatData);

            Assert.That(doubleDataByFastWay, Is.EquivalentTo(doubleDataByStandardWay));
        }
        [Test]
        public void SpecialCasesTest()
        {
            float[] floatData = {0.0f, +Single.NaN, -Single.NaN, Single.MaxValue, Single.MinValue, Single.NegativeInfinity, Single.PositiveInfinity, };
            double[] doubleDataByStandardWay = StandardWay.CreateDoubleArray(floatData);
            double[] doubleDataByFastWay = FastWay.CreateDoubleArray(floatData);

            Assert.That(doubleDataByFastWay, Is.EquivalentTo(doubleDataByStandardWay));
        }
        [Test]
        public void SmallNumbers()
        {
            float[] floatData = { 0.0f, 1, 1e-1f, 1e-2f, 1e-3f, 1e-4f, 1e-5f };
            double[] doubleDataByStandardWay = StandardWay.CreateDoubleArray(floatData);
            double[] doubleDataByFastWay = FastWay.CreateDoubleArray(floatData);

            Assert.That(doubleDataByFastWay, Is.EquivalentTo(doubleDataByStandardWay));
        }

        [Explicit, Test]
        public void PerformanceTestByBenchmarkDotNet()
        {
            var summary = BenchmarkRunner.Run<BenchmarkData>();
        }

        [Explicit, Test]
        public void PerformanceTestByBenchmarkIt()
        {
            BenchmarkData benchmarkData = new BenchmarkData();
            benchmarkData.SetupData();

            BenchmarkIt.Benchmark.This("StandardWay", () =>
            {
                double[] standardResult = benchmarkData.StandardWay();
            })
            .Against.This("FastWay", () =>
            {
                double[] fastResult = benchmarkData.FastWay();
            })
            .For(1000)
            .Iterations()
            .PrintComparison();
        }
    }

    public class BenchmarkData
    {
        private float[] m_floatData;
        [Setup]
        public void SetupData()
        {
            Fixture fixture = new Fixture();
            m_floatData = fixture.CreateMany<float>(10000).ToArray();
        }

        [Benchmark]
        public double[] StandardWay()
        {
            return csharp_tips.StandardWay.CreateDoubleArray(m_floatData);
        }
        [Benchmark]
        public double[] FastWay()
        {
            return csharp_tips.FastWay.CreateDoubleArray(m_floatData);
        }
    }

    class StandardWay
    {
        public static double[] CreateDoubleArray(float[] source)
        {
            double[] destination = new double[source.Length];
            Copy(source, 0, destination, 0, source.Length);
            return destination;
        }
        static void Copy(float[] source, int sourceIndex, double[] destination, int destIndex, int count)
        {
            for (int i = 0; i < count; i++)
            {
                destination[i + destIndex] = GetDoubleFromFloatWithoutRoundingError(source[i + sourceIndex]);
            }
        }
        static double GetDoubleFromFloatWithoutRoundingError(float f)
        {
            if (float.IsNaN(f)) // SafeGuards against conversion errors
                return double.NaN;
            if (Math.Abs(f) > (float)decimal.MaxValue)
                return f;

            decimal d = (decimal)f;
            return (double)d;
        }
    }

    class FastWay
    {
        public static unsafe double[] CreateDoubleArray(float[] source)
        {
            float* fp0;
            double* dp0;
            int count = source.Length;

            fixed (float* fp1 = source)
            {
                fp0 = fp1;

                double[] newDbl = new double[count];
                fixed (double* dp1 = newDbl)
                {
                    dp0 = dp1;
//                    for (int i = 0; i < numCh; i++)
//                    {
                        //Parallel.For(0, count, (j) =>
                        for (int j = 0; j < count; j++)
                        {
                            dp0[j] = (double)fp0[j];
                        }
                        //});
                        return newDbl;
                    }
//                }
            }
        }
    }
}