using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ctor.location.framework
{
    public class SplitterHelper
    {
        static readonly Regex csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);
        public static string[] Splitter(string inputLine)
        {
            return inputLine.Contains("\"") ? SplitCSV(inputLine) : inputLine.Split(',');
            //return Regex.Split(inputLine, ",(?=([^\"]*\"[^\"]*\")*[^\"]*$)");
        }
        public static string[] SplitCSV(string input)
        {            
            List<string> list = new List<string>();
            foreach (Match match in csvSplit.Matches(input))
            {
                string curr = match.Value;
                if (0 == curr.Length)
                {
                    list.Add("");
                }

                list.Add(curr.TrimStart(','));
            }

            return list.ToArray();
        }
    }
}