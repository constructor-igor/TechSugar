using System.IO;
using System.Text;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class ReadingFileTests
    {
        [Test, Explicit]
        public void Test()
        {
            string fileName = @"d:\@Temp\test.txt";
            string fileNameMonitor = @"d:\@Temp\test-monitoring.txt";
            int index = 0;
            while (index++ < 100000)
            {
                string content = ImportFileContent(fileName);
                ExportContentTo(fileNameMonitor, content);
            }
        }

        public string ImportFileContent(string fileNamePath)
        {
            using (var fs = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs, Encoding.Default))
            {
                string content = sr.ReadToEnd();
                return content;
            }
        }

        public void ExportContentTo(string fileNamePath, string content)
        {
            if (File.Exists(fileNamePath))
                File.AppendAllText(fileNamePath, content);
            else
                File.WriteAllText(fileNamePath, content);
        }
    }
}