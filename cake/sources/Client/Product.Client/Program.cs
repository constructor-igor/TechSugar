using System;
using Product.Server;

namespace Product.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            double x = 1;
            double y = 2;
            double r = Engine.Add(x, y);
            Console.WriteLine("{0} + {1} = {2}", x, y, r);
        }
    }
}
