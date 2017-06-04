using System;
using System.Threading;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class RetryTests
    {
        [TestCase(false, 0)]
        [TestCase(true, 0)]
        [TestCase(true, 1)]
        [TestCase(true, 2)]
        public void Test(bool raiseException, int retryCounter)
        {
            Foo(raiseException, retryCounter);
        }

        void Foo(bool raiseException, int retryCounter)
        {
            Console.WriteLine("Foo [Start]");
            Thread t = new Thread(delegate()
            {
                RetryHelper.Run(()=> { Foo_BusinessLogic(raiseException); }, retryCounter);
            });
            t.Start();

            t.Join();
            Console.WriteLine("Foo [Complete]");
        }

        void Foo_BusinessLogic(bool raiseException)
        {
            Console.WriteLine("Foo_BusinessLogic");
            Thread.Sleep(1000);
            if (raiseException)
                throw new Exception();
        }
    }

    public class RetryHelper
    {
        public static void Run(Action action, int retryCounter)
        {
            while (retryCounter>=0)
            {
                try
                {
                    action();
                    retryCounter = -1;
                }
                catch (Exception e)
                {
                    Console.WriteLine("RetryHelper [Exception], counter: {0}", retryCounter);
                    retryCounter--;
                }
            }
        }
    }

}