using System;
using System.Threading;
using DesignPatterns.Observer;

namespace consoleApplication
{
    class Program
    {
        static readonly BillingService BillingService = new BillingService();
        static void Main(string[] args)
        {
            ExecuteTest();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.WriteLine("before sleep");
            Thread.Sleep(5000);
            Console.WriteLine("after sleep");
        }

        private static void ExecuteTest()
        {
            ObserverTests observerTests = new ObserverTests();
            observerTests.RunTest(true, BillingService);
        }
    }
}
