using System;
using System.Diagnostics;

namespace PerformanceLab
{
    class Program
    {
        public const int LoopSize = 1000000;

        static void Main(string[] args)
        {
            IEnumService enumService = new OptimizedEnumService();
            RunTest_NotEnumValue(enumService);
            RunTest_YesEnumValue(enumService);
        }

        static void RunTest_NotEnumValue(IEnumService enumService)
        {
            RunTest(2, enumService);
        }
        static void RunTest_YesEnumValue(IEnumService enumService)
        {
            RunTest(1, enumService);
        }
        static void RunTest(int intValue, IEnumService enumService)
        {
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < LoopSize; i++)
            {
                EnumDataTypes.Enum1 value;
                bool f = enumService.IntValue2EnumValue<EnumDataTypes.Enum1>(intValue, out value);
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }
    }
}
