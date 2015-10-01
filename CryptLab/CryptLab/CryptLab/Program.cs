using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CryptLab
{
    class Program
    {
        static void Main(string[] args)
        {
            string cryptFilePath = args[0];
            Console.WriteLine("Crypt file path: {0}", cryptFilePath);

            string cryptFileContent = File.ReadAllText(cryptFilePath).ToLower();

            string cryptText = Resource.cryptoText;

            FrequencyCharInfo publicCharInfo = new FrequencyCharInfo();
            StatisticCharInfo customerStatisticInfo = new StatisticCharInfo();
            customerStatisticInfo.ProcessCryptData(cryptFileContent);
            customerStatisticInfo.WriteToConsole();
            FrequencyCharInfo customerCharInfo = customerStatisticInfo.CreateFrequencyCharInfo();

            // n -> a
            // nc, nx -> am, an, as, at
            // xzr -> the

            // ? ncf --> and or any
            // ? ur --> be

            SubstitutionEngine substitutionEngine = new SubstitutionEngine(publicCharInfo, customerCharInfo);
            substitutionEngine.AddPair('n', 'a');
            substitutionEngine.AddPair('j', 'o');

//            substitutionEngine.AddPair('x', 't');            
//            substitutionEngine.AddPair('r', 'e');
//            substitutionEngine.AddPair('z', 'h');
//            substitutionEngine.AddPair('f', 'd');

            substitutionEngine.AddPair("xzryr nyr", "there are");
            substitutionEngine.AddPair("fqeeryrcx", "different");
            substitutionEngine.AddPair("rcojfqcm", "encoding");
            substitutionEngine.AddPair("cvhury", "number");
            substitutionEngine.AddPair("xpbrw", "types");
            substitutionEngine.AddPair("aqxz", "with");
            substitutionEngine.AddPair("wrtrynl", "several");
            substitutionEngine.AddPair("blnqcxrgx", "plaintext");
            substitutionEngine.AddPair("wrsvrcor", "sequence");

            Dictionary<char, char> substitution = substitutionEngine.CreateSubstitution();

            foreach (KeyValuePair<char, char> keyValuePair in substitution)
            {
                Console.WriteLine("{0} ({1:N}) --> {2} ({3:N})", 
                    keyValuePair.Key, customerStatisticInfo.CustomerCharsFrequency[keyValuePair.Key].Frequency,
                    keyValuePair.Value, publicCharInfo.PublicCharsFrequency[keyValuePair.Value]);
            }
            Console.WriteLine();
            Console.WriteLine("Decoded text:");

            StringBuilder plainText = new StringBuilder();
            foreach (char c in cryptFileContent)
            {
                plainText.Append(substitution.ContainsKey(c) ? substitution[c] : c);
            }

            Console.WriteLine(plainText.ToString());
        }
    }
}
