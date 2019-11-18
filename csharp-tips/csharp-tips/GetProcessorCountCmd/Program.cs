using System;

namespace GetProcessorCountCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Environment.ProcessorCount: {Environment.ProcessorCount}");
            Print();
            Console.WriteLine("Press <Enter> to exit.");
            Console.ReadLine();
        }

        static void Print()
        {
            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
            {
                Console.WriteLine("Number Of Physical Processors: {0} ", item["NumberOfProcessors"]);
            }

            // Number of Cores

            int coreCount = 0;
            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_Processor").Get())
            {
                coreCount += int.Parse(item["NumberOfCores"].ToString());
            }

            Console.WriteLine("Number Of Cores: {0}", coreCount);

            // Number of Logical Processors

            foreach (var item in new System.Management.ManagementObjectSearcher("Select * from Win32_ComputerSystem").Get())
            {
                Console.WriteLine("Number Of Logical Processors: {0}", item["NumberOfLogicalProcessors"]);
            }
        }
    }
}
