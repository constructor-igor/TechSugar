using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
            List<DataItem> dataItems = CreateDataItems();

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

        [Test]
        public void WriteIncludeVersionLine()
        {
            DataHeader header = new DataHeader {Version = "Ver=1"};
            DataObject dataObject = new DataObject { Version = header, Items = CreateDataItems() };
            using (TemporaryFile csvFile = new TemporaryFile())
            {
                using (TextWriter textWriter = new StreamWriter(csvFile.FileName))
                {
                    CsvConfiguration configuration = new CsvConfiguration { Delimiter = " " };
                    using (CsvWriter csvWriter = new CsvWriter(textWriter, configuration))
                    {
                        csvWriter. WriteRecord(dataObject);
                    }
                }

                List<string> expectedContent = new List<string> { "Ver=1", "FirstId SecondId", "10 20", "10.1 20.1" };
                IEnumerable<string> actualContent = File.ReadLines(csvFile.FileName);
                Assert.That(actualContent, Is.EquivalentTo(expectedContent));
            }
        }

        #region private methods
        private static List<DataItem> CreateDataItems()
        {
            List<DataItem> dataItems = new List<DataItem>
            {
                new DataItem {FirstId = "10", SecondId = "20"},
                new DataItem {FirstId = "10.1", SecondId = "20.1"}
            };
            return dataItems;
        }
        #endregion

        public sealed class DataObjectMap : CsvClassMap<DataObject>
        {
            public DataObjectMap()
            {
                Map(m => m.Version);
                Map(m => m.Items);
            }
        }
    }

    public class DataObject
    {
        public DataHeader Version { get; set; }
        public List<DataItem> Items { get; set; }

        public DataObject()
        {
            Items = new List<DataItem>();
        }
    }

    public class DataHeader
    {
        public string Version { get; set; }
    }
    public class DataItem
    {
        public string FirstId { get; set; }
        public string SecondId { get; set; }
    }
}
