using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using NUnit.Framework;

namespace csharp_tips
{
    public class ParameterDataObject3
    {
        public string Name { get; set; }
        public bool Flag1 { get; set; }
        public bool Flag2 { get; set; }

        public ParameterDataObject3() { }
        public ParameterDataObject3(string name, bool flag1, bool flag2)
        {
            Name = name;
            Flag1 = flag1;
            Flag2 = flag2;
        }
    }
    public class DataObject3
    {
        public List<ParameterDataObject3> Parameters { get; set; }

        public DataObject3()
        {
            
        }
    }
    [TestFixture]
    public class XmlSerialization3Tests
    {
        [Test]
        public void Test()
        {
            DataObject3 dataObject3 = new DataObject3();
            dataObject3.Parameters = new List<ParameterDataObject3>();
            dataObject3.Parameters.Add(new ParameterDataObject3("parameter1", false, true));
            dataObject3.Parameters.Add(new ParameterDataObject3("parameter2", false, true));

            StringBuilder xmlContent = new StringBuilder();
            GenericSerializer3<DataObject3> serializer = new GenericSerializer3<DataObject3>();
            serializer.Serialize(new StringWriter(xmlContent), dataObject3);

            Console.WriteLine("xml content:");
            Console.WriteLine("{0}", xmlContent);
        }
    }

    public class GenericSerializer3<T> : XmlSerializer where T : new()
    {
        public GenericSerializer3() : base(typeof(T)) { }
    }

}