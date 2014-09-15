using System;
using System.Collections.Generic;
using System.Linq;

namespace RxBasic
{
    class Program
    {
        static void Main()
        {
            PersonalForeachImplementation();
            Console.WriteLine();
            Console.WriteLine();
            StandardForeachImplementation();
            Console.WriteLine();
            Console.WriteLine();
            LinqSample();
        }

        private static void StandardForeachImplementation()
        {
            Console.WriteLine("standard 'foreach' implementation");
            var jobExecuter = new JobExecuter();
            jobExecuter.Run();
            foreach (WorkerResult workerResult in jobExecuter.GetResults())
            {
                Console.WriteLine("worker: {0}", workerResult.Caption);
            }
            foreach (WorkerResult workerResult in jobExecuter.GetResults())
            {
                Console.WriteLine("worker: {0}", workerResult.Caption);
            }
        }

        private static void PersonalForeachImplementation()
        {
            Console.WriteLine("personal 'foreach' implementation");
            var jobExecuter = new JobExecuter();
            jobExecuter.Run();

            IEnumerable<WorkerResult> results = jobExecuter.GetResults();
            using (IEnumerator<WorkerResult> enumerator = results.GetEnumerator())
            {
                enumerator.Reset();
                do
                {
                    Console.WriteLine("worker: {0}", enumerator.Current.Caption);
                } while (enumerator.MoveNext());
            }
        }

        private static void LinqSample()
        {
            Console.WriteLine("Linq samples");
            var jobExecuter = new JobExecuter();
            jobExecuter.Run();
            Console.WriteLine("any: {0}", jobExecuter.GetResults().Any());
            foreach (WorkerResult workerResult in jobExecuter.GetResults())
            {
                Console.WriteLine("worker: {0}", workerResult.Caption);
            }   
        }
    }
}
