using System;
using ILNumerics;

namespace ILNumericsIntro
{
    public class ILArraySample : ILMath
    {
        public void Test()
        {
            ILArray<double> A = rand(10, 20);
            ILArray<double> B = A * 30 + 100;
            ILLogical C = any(multiply(B, B.T));
            Console.Out.Write(-B);
            Console.ReadKey();
        }
    }
}