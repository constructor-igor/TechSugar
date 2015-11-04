using csharp_tips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace csharp_console
{
    class Program
    {
        static void Main(string[] args)
        {
            //POP3Client.POP3client demoPop3Client = new POP3Client.POP3client();
            //Console.WriteLine(demoPop3Client.connect("pop.gmail.com"));
            OpenPopNetClientTests tests = new OpenPopNetClientTests();
            tests.Test();
        }
    }
}
