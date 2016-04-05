using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BusinessLogic;
using NUnit.Framework;

namespace BenchmarkDotNetLab
{
    [TestFixture]
    public class BenchmarkProblemSample
    {
        private const string TEXT = "the test shows benchmark samples";
        private const string TERMIN = "benchmark";

        [Test]
        public void RunBenchmark()
        {
            var summary = BenchmarkRunner.Run<BenchmarkProblemSample>();
        }

        [Benchmark()]
        public void IndexOfSample()
        {
            BusinessLogicImpl.IndexOfSample();
        }
        [Benchmark]
        public void ContainsSample()
        {
            BusinessLogicImpl.ContainsSample();
        }

        [Test]
        public void SimpleTest()
        {
            BusinessLogicImpl.IndexOfSample();
            BusinessLogicImpl.ContainsSample();
        }
    }
}