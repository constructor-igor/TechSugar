using BenchmarkIt;
using NUnit.Framework;

namespace BenchmarkItLab
{
    [TestFixture]
    public class BenchmarkItSample
    {
        [Test]
        public void Sample()
        {
            Benchmark
                .This("string.Contains", () => "abcdef".Contains("ef"))
            .Against
                .This("string.IndexOf", () => "abcdef".IndexOf("ef"))
                .WithWarmup(1)
                .For(5)
                .Seconds().PrintComparison();
        }
    }
}
