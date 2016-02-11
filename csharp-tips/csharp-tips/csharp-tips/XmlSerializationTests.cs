using System;
using System.IO;
using System.Xml.Serialization;
using NUnit.Framework;

namespace csharp_tips
{
    public class DataObject
    {
        public bool Flag { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [TestFixture]
    public class XmlSerializationTests
    {
        [Test]
        public void Test()
        {
            string temporaryFile = Path.GetTempFileName();
            try
            {
                DataObject dataObject = new DataObject {Flag = true, Id = 103, Name = "Joy"};

                XmlSerializer serializer = new XmlSerializer(typeof(DataObject));
                using (TextWriter writer = new StreamWriter(temporaryFile))
                {
                    serializer.Serialize(writer, dataObject);
                }
                string fileContent = File.ReadAllText(temporaryFile);
                Console.WriteLine(fileContent);

                using (TextReader reader = new StreamReader(temporaryFile))
                {
                    dataObject = (DataObject) serializer.Deserialize(reader);

                    Console.WriteLine("{0}, {1}, {2}", dataObject.Flag, dataObject.Id, dataObject.Name);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally 
            {
                File.Delete(temporaryFile);
            }
        }
    }
}