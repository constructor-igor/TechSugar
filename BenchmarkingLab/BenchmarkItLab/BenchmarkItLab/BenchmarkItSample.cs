using System.Linq;
using System.Text;
using BenchmarkIt;
using NUnit.Framework;

namespace BenchmarkItLab
{
    [TestFixture]
    public class BenchmarkItSample
    {
        [Test]
        public void String_Contains_vs_IndexOf()
        {
            Benchmark
                .This("string.Contains", () => "abcdef".Contains("ef"))
            .Against
                .This("string.IndexOf", () => "abcdef".IndexOf("ef"))
            .WithWarmup(1)
            .For(5)
            .Seconds().PrintComparison();
        }

        [Test]
        public void StringBuilderCapacity()
        {
            var list = Enumerable.Range(1, 256).ToList();
            Benchmark
                    .This("capacity 1", () =>
                    {
                        StringBuilder sb = new StringBuilder(1);
                        list.ForEach(index => sb.Append("0"));
                    })
            .Against
                    .This("capacity default(16)", () =>
                    {
                        StringBuilder sb = new StringBuilder();
                        list.ForEach(index => sb.Append("0"));
                    })
            .Against
                    .This("capacity 256", () =>
                    {
                        StringBuilder sb = new StringBuilder(256);
                        list.ForEach(index => sb.Append("0"));
                    })
            .Against
                    .This("capacity 1024", () =>
                    {
                        StringBuilder sb = new StringBuilder(1024);
                        list.ForEach(index => sb.Append("0"));
                    })
            .WithWarmup(2)
            .For(100000)
            .Iterations()            
            .PrintComparison();
        }
    }
}
