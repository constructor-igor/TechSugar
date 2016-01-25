using System;
using System.Threading;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class ProgressSamples
    {
        private const int SEC1 = 1000;
        [Test]
        public void Test()
        {
            Do(5, new Progress<int>(i=>Console.WriteLine("progress {0}", i)));
        }

        void Do(int iterationsCount, IProgress<int> progress)
        {
            for (int i = 0; i < iterationsCount; i++)
            {
                Thread.Sleep(SEC1);
                progress.Report(i);
            }
        }
    }
}