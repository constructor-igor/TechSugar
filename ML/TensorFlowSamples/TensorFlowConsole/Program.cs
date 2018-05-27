using System;
using TensorFlow;

namespace TensorFlowConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Sample1();
        }

        private static void Sample1()
        {
            using (var session = new TFSession())
            {
                var graph = session.Graph;

                var a = graph.Const(2);
                var b = graph.Const(3);
                Console.WriteLine("a=2 b=3");

                // Add two constants
                var addingResults = session.GetRunner().Run(graph.Add(a, b));
                var addingResultValue = addingResults.GetValue();
                Console.WriteLine("a+b={0}", addingResultValue);

                // Multiply two constants
                var multiplyResults = session.GetRunner().Run(graph.Mul(a, b));
                var multiplyResultValue = multiplyResults.GetValue();
                Console.WriteLine("a*b={0}", multiplyResultValue);
            }
        }
    }
}
