using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Hash
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string input = "00 abc DE ? 123";
            string hash = CreateHash(input);
            Console.WriteLine($"input: {input}, result: {hash}");
        }
        static string CreateHash(string input){
            var normalied = input
                .ToCharArray()
                .Where(c=>Regex.IsMatch(c.ToString(), "[a-z,A-Z,0-9]"))
                .Select(c=>char.ToLower(c));
            // var cleanInputs = input.ToCharArray().Where(c=>Regex.IsMatch(c.ToString(), "[a-z,A-Z,0-9]"));
            // var lowerInputs = cleanInputs.Select(c=>char.ToLower(c));
            // string normalizedInput = new string(lowerInputs.ToArray()).TrimStart(new char[]{'0'});
            string normalizedInput = new string(normalied.ToArray()).TrimStart(new char[]{'0'});
            string shaInput = ComputeSha256Hash(normalizedInput);
            string hex = ConvertToHex(shaInput);
            return shaInput;
        }

        static string ConvertToHex(string input){
            byte[] retval = System.Text.Encoding.ASCII.GetBytes(input);
            string hexResult = BitConverter.ToString(retval).Replace("-", " 0x");
            return hexResult;
        }
        static string ComputeSha256Hash(string rawData)  
        {  
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));  
  
                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();  
                for (int i = 0; i < bytes.Length; i++)  
                {  
                    builder.Append(bytes[i].ToString("x2"));  
                }  
                return builder.ToString();  
            }  
        }  
    }
}
