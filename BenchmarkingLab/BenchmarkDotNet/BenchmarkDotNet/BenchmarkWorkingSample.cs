using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using NUnit.Framework;

namespace BenchmarkDotNetLab
{
    [TestFixture]
    public class BenchmarkWorkingSample
    {
        private const string TEXT = "the test shows benchmark samples";
        private const string TERMIN = "benchmark";

        [Test]
        public void RunBenchmark()
        {
            var summary = BenchmarkRunner.Run<BenchmarkWorkingSample>();
//            BenchmarkSettings settings = new BenchmarkSettings(warmupIterationCount:1, targetIterationCount:10);
//            new BenchmarkRunner().RunCompetition(new BenchmarkWorkingSample(), settings);
        }

        [Benchmark]
        public void IndexOfSample()
        {
            bool contains = TEXT.IndexOf(TERMIN, StringComparison.Ordinal) >= 0;
        }
        [Benchmark]
        public void ContainsSample()
        {
            bool contains = TEXT.Contains(TERMIN);
        }

        [Test]
        public void SimpleTest()
        {
            bool contains1 = TEXT.IndexOf(TERMIN, StringComparison.Ordinal) >= 0;
            Assert.That(contains1, Is.True);

            bool contains2 = TEXT.Contains(TERMIN);
            Assert.That(contains2, Is.True);
        }
    }
}
