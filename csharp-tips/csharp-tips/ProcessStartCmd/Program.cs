using System;
using System.IO;
using NFile.Framework;

namespace ProcessStartCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            using (TemporaryFile file = new TemporaryFile("test.unknown"))
            {
                File.WriteAllText(file.FileName,"4;t");
                System.Diagnostics.Process.Start(file.FileName);
                Console.WriteLine("Press <Enter> to exit.");
                Console.ReadLine();
            }
        }
    }
}
