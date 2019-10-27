using System;

/*
 *  https://stackoverflow.com/questions/229565/what-is-a-good-pattern-for-using-a-global-mutex-in-c
 *
 *
 */

namespace GlobalMutexSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("GlobalMutexSample started");
            using (SingleGlobalInstance globalInstance = new SingleGlobalInstance(1000))
            {
                if (globalInstance.HasHandle)
                {
                    Console.WriteLine("Perform work.");
                    Console.WriteLine("Press <Enter> to complete the application.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Work cannot be performed, because other instance of the application runs.");
                }
            }
            Console.WriteLine("GlobalMutexSample finished");
        }
    }
}
