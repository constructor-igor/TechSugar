using System;
using System.Diagnostics;
using System.Threading;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class StackTraceSamples
    {
        [Test]
        public void Test()
        {            
            ShowStackTrace();
            Assert.Pass();
        }
        [Test]
        public void TestThread()
        {
            Thread thread = new Thread(ShowStackTrace);
            thread.Start();
            while (!thread.IsAlive);
            Thread.Sleep(100);
            thread.Abort();
            thread.Join();
            Assert.Pass();
        }

        private void ShowStackTrace()
        {
            StackTrace stackTrace = new StackTrace();
            Console.WriteLine("stack Trace: " + stackTrace);
        }
    }
}