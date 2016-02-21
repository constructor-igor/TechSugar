using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class XmlSerialization2Tests
    {
        public enum Options { None, One, Two, ConstructorDefault, AttributeDefault }
        public class DataObject
        {
            public bool Flag { get; set; }
            public int Id { get; set; }
            public string Name { get; set; }
            [XmlAttribute("MyOptions")]
            [DefaultValue(Options.AttributeDefault)]
            public Options Options { get; set; }

            public DataObject()
            {
                Options = Options.ConstructorDefault;
            }
        }

        [Test]
        public void Test()
        {
            XMLSerializerCustomReadonly<DataObject> serializer = new XMLSerializerCustomReadonly<DataObject>();

            StringBuilder content = new StringBuilder();
            content.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            content.AppendLine("<DataObject xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
            content.AppendLine("<Flag>true</Flag>");
            content.AppendLine("<Id>103</Id>");
            content.AppendLine("<Name>Joy</Name>");
            content.AppendLine("</DataObject>");

            using (StringReader reader = new StringReader(content.ToString()))
            {
                DataObject actualObject = serializer.Deserialize(reader);
                Assert.That(actualObject.Options, Is.EqualTo(Options.ConstructorDefault));
            }
        }
    }

    public class XMLSerializerCustomReadonly<T> : XMLSerializer<T> where T : new()
    {
        private static readonly XmlAttributeOverrides XOver;

        static XMLSerializerCustomReadonly()
        {
            XOver = new XmlAttributeOverrides();
            PrepareXOver(XOver);
        }

        public XMLSerializerCustomReadonly()
            : base()
        {
            xmlSerializer = new XmlSerializer(typeof(T), XOver);
            xmlDeserializer = new XmlSerializer(typeof(T));
        }

        public XMLSerializerCustomReadonly(IEnumerable<string> ignores)
        {
            XmlAttributeOverrides over = new XmlAttributeOverrides();
            PrepareXOver(over);
            List<Type> types = GetTypesOfT();
            XmlAttributes xmlAttributes = new XmlAttributes { XmlIgnore = true };

            foreach (var type in types)
            {
                foreach (var s in ignores)
                {
                    over.Add(type, s, xmlAttributes);
                }
            }

            xmlSerializer = new XmlSerializer(typeof(T), over);
            xmlDeserializer = new XmlSerializer(typeof(T));
        }

        private static void PrepareXOver(XmlAttributeOverrides xover)
        {
            XmlAttributes xmlAttributes = new XmlAttributes { XmlIgnore = true };

            List<Type> types = GetTypesOfT();
            foreach (var type in types)
            {
                foreach (var propertyInfo in type.GetProperties())
                {
                    if (propertyInfo.GetCustomAttributes(typeof(XmlReadonlyAttribute), true).Length > 0)
                    {
                        xover.Add(type, propertyInfo.Name, xmlAttributes);
                    }
                }
            }
        }

        private static List<Type> GetTypesOfT()
        {
            List<Type> types = new List<Type>() { typeof(T) };

            if (typeof(T).IsGenericType)
            {
                types.AddRange(typeof(T).GetGenericArguments());
            }
            return types;
        }
    }

    public class XMLSerializer<T> where T : new()
    {
        protected XmlSerializer xmlSerializer;
        protected XmlSerializer xmlDeserializer;

        public XMLSerializer()
        {
            xmlSerializer = new XmlSerializer(typeof(T));
            xmlDeserializer = new XmlSerializer(typeof(T));
        }

        public void Serialize(StringWriterWithEncoding writer, T t)
        {
            xmlSerializer.Serialize(writer, t);
        }

        public void Serialize(string ksfName, T fileDetails)
        {
            if (File.Exists(ksfName))
            {
                FileInfo fi = new FileInfo(ksfName);
                fi.IsReadOnly = false;
            }
            using (var writer = new StreamWriter(ksfName))
            {
                xmlSerializer.Serialize(writer, fileDetails);
            }
        }

        public T Deserialize(string xmlPath)
        {
            using (var fs = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            {
                using (XmlReader r = XmlReader.Create(fs))
                {
                    return (T)xmlDeserializer.Deserialize(r);
                }
            }
        }

        public T DeserializeLines(string[] lines)
        {
            return (T)xmlDeserializer.Deserialize(new StringReader(string.Join("", lines)));
        }

        public bool CanDeserializeLines(string[] lines)
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(string.Join("", lines))))
            {
                return xmlSerializer.CanDeserialize(reader);
            }
        }

        public T Deserialize(StringReader xml)
        {
            return (T)xmlDeserializer.Deserialize(xml);
        }

        public bool CanDeserialize(string fileName)
        {
            using (XmlReader reader = XmlReader.Create(fileName))
            {
                return xmlSerializer.CanDeserialize(reader);
            }
        }

        public bool CanDeserialize(StringReader memoryDecrypt)
        {
            using (XmlReader reader = XmlReader.Create(memoryDecrypt))
            {
                return xmlSerializer.CanDeserialize(reader);
            }
        }
    }

    public class StringWriterWithEncoding : StringWriter
    {
        readonly Encoding encoding;

        public StringWriterWithEncoding(Encoding encoding)
        {
            this.encoding = encoding;
        }

        public StringWriterWithEncoding(StringBuilder builder, Encoding encoding)
            : base(builder)
        {
            this.encoding = encoding;
        }

        public override Encoding Encoding
        {
            get { return encoding; }
        }
    }

    public class XmlReadonlyAttribute : Attribute
    {
    }

}