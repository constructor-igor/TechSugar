using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class TasksTests
    {
        [Test]
        public void CreateSimpleTask_New()
        {
            Task<double> task = new Task<double>(()=>
            {
                Console.WriteLine("task started");
                Thread.Sleep(5*1000);
                return 0.0;
            });
            task.ContinueWith(result =>
            {
                Console.WriteLine("task completed");
            });
            task.Start();

            Console.WriteLine("before wait");
            task.Wait();
            Console.WriteLine("after wait");
        }
        [Test]
        public void CreateSimpleTask_FactoryStartNew()
        {
            Task<double> task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("task started");
                Thread.Sleep(5 * 1000);
                return 0.0;
            });
            task.ContinueWith(result =>
            {
                Console.WriteLine("task completed");
            });

            Console.WriteLine("before wait");
            task.Wait();
            Console.WriteLine("after wait");
        }
    }
}