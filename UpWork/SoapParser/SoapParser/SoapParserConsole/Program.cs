using System;
using System.IO;

namespace SoapParserConsole
{
    class Program
    {
        static int Main(string[] args)
        {
            string fileName = args.Length > 0 ? args[1] : @"data\soapExample2.txt";
            bool fileExists = File.Exists(fileName);
            Console.WriteLine($"File {fileName} exists: {fileExists}");

            if (!fileExists)
            {
                Console.WriteLine($"Exit, because file {fileName} not found.");
                return -1;
            }

            SoapParser parser = new SoapParser(fileName);
            string sessionId = parser.GetSessionID();
            Console.WriteLine($"Found value of 'SessionID' is {sessionId}");
            return 0;
        }
    }
}
