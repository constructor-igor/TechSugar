using System;
using RxBasic.EnumerableSamples;
using RxBasic.ObservableSamples;

namespace RxBasic
{
    internal class Program
    {
        private static void Main()
        {
            //EnumerableSamples();

            ObservableSamples();
        }       

        private static void EnumerableSamples()
        {
            EnumerableUseCases.CustomerNaiveForeachImplementation();
            Console.WriteLine();
            Console.WriteLine();

            EnumerableUseCases.StandardForeachImplementation();
            Console.WriteLine();
            Console.WriteLine();

            EnumerableUseCases.LinqSample();
            Console.WriteLine();
            Console.WriteLine();

            //EnumerableUseCases.ProblemOfNaiveImplementation();
            //Console.WriteLine();
            //Console.WriteLine();

            EnumerableUseCases.SolveOfTheProblemByCorrectImplementation();
        }

        private static void ObservableSamples()
        {
            var observableJobExecuter = new CustomerObservableJobExecuter();
            IObserver<WorkerResult> workerObserver = new WorkerObserver();
            using (observableJobExecuter.Subscribe(workerObserver))
            {
                observableJobExecuter.Run();
                observableJobExecuter.Run();
            }
            observableJobExecuter.Run();
        }
    }
}
