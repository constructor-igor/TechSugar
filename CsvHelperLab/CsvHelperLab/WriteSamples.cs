using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using NFile.Framework;
using NUnit.Framework;

namespace CsvHelperLab
{
    [TestFixture]
    public class WriteSamples
    {
        [Test]
        public void WriteConstantParametersNumber()
        {
            List<DataItem> dataItems = new List<DataItem>
            {
                new DataItem {FirstId = "10", SecondId = "20"},
                new DataItem {FirstId = "10.1", SecondId = "20.1"}
            };

            using (TemporaryFile csvFile = new TemporaryFile())
            {
                using (TextWriter textWriter = new StreamWriter(csvFile.FileName))
                {
                    CsvConfiguration configuration = new CsvConfiguration {Delimiter = " "};
                    using (CsvWriter csvWriter = new CsvWriter(textWriter, configuration))
                    {                        
                        csvWriter.WriteRecords(dataItems);
                    }
                }

                List<string> expectedContent = new List<string> {"FirstId SecondId", "10 20", "10.1 20.1"};
                IEnumerable<string> actualContent = File.ReadLines(csvFile.FileName);
                Assert.That(actualContent, Is.EquivalentTo(expectedContent));
            }
        }
    }

    public class DataItem
    {
        public string FirstId { get; set; }
        public string SecondId { get; set; }
    }
}
