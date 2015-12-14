using System;
using System.Threading;
using NUnit.Framework;

namespace NUnit_v3_samples
{
    [TestFixture]
    public class NUnitIssue1102
    {
        [OneTimeSetUp]
        public virtual void RunBeforeAnyTests()
        {
            string testName = TestContext.CurrentContext.Test.FullName;
            Console.WriteLine("{0}: RunBeforeAnyTests ({1})", Thread.CurrentThread.ManagedThreadId, testName);            
        }

        [Test]
        public void Test1()
        {
            string testName = TestContext.CurrentContext.Test.FullName;
            Console.WriteLine("{0}: RunBeforeAnyTests ({1})", Thread.CurrentThread.ManagedThreadId, testName);
        }

        [Test]
        public void Test2()
        {
            string testName = TestContext.CurrentContext.Test.FullName;
            Console.WriteLine("{0}: RunBeforeAnyTests ({1})", Thread.CurrentThread.ManagedThreadId, testName);
        }
    }
}