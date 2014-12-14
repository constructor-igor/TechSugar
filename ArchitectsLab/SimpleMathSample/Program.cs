using System;

namespace SimpleMathSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var customMath = new CustomMath();
            var algorithm = new Algorithm();
            double r = algorithm.Run(customMath, 10, 20, 100);
            Console.WriteLine("r={0}", r);
        }
    }
}
