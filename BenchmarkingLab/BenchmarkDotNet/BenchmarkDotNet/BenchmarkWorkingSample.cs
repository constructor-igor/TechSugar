using System;
using BenchmarkDotNet;
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
            new BenchmarkRunner().RunCompetition(new BenchmarkWorkingSample());
        }

        [Benchmark]
        [Test, Explicit]
        public void IndexOfSample()
        {
            bool contains = TEXT.IndexOf(TERMIN, StringComparison.Ordinal) >= 0;
            Assert.That(contains, Is.True);
        }
        [Benchmark]
        [Test, Explicit]
        public void ContainsSample()
        {
            bool contains = TEXT.Contains(TERMIN);
            Assert.That(contains, Is.True);
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
