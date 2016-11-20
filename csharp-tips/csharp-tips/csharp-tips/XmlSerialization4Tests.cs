using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using NUnit.Framework;

namespace csharp_tips
{
    public interface IItemDataObject4
    {
        int ItemValue { get; }
    }

    public class ItemDataObject4 : IItemDataObject4
    {
        #region IItemDataObject4
        public int ItemValue { get; set; }
        #endregion

        private ItemDataObject4()    // for serialization only
        {
            
        }
        public ItemDataObject4(int itemValue)
        {
            ItemValue = itemValue;
        }
    }
    public class DataObject4
    {
        public int IntValue = 3;
        public double DoubleValue = 10.5;
        public int[] IntBuffer;
        public ItemDataObject4 ItemDataObject4;
        public DataObject4()
        {
            ItemDataObject4 = new ItemDataObject4(40);
        }
    }

    [TestFixture]
    public class XmlSerialization4Tests
    {
        [Test]
        public void Test()
        {
            DataObject4 dataObject = new DataObject4 {DoubleValue = 20.6, IntBuffer = new[] {0, 1, 2, 3, 4}};

            StringBuilder xmlContent = new StringBuilder();
            GenericSerializer4<DataObject4> serializer = new GenericSerializer4<DataObject4>();
            serializer.Serialize(new StringWriter(xmlContent), dataObject);

            Console.WriteLine("xml content:");
            Console.WriteLine("{0}", xmlContent);

            DataObject4 actualDataObject = (DataObject4) serializer.Deserialize(new StringReader(xmlContent.ToString()));

            Assert.That(actualDataObject.IntBuffer, Is.EquivalentTo(dataObject.IntBuffer));
            Assert.That(actualDataObject.ItemDataObject4.ItemValue, Is.EqualTo(dataObject.ItemDataObject4.ItemValue));
        }
    }

    public class GenericSerializer4<T> : XmlSerializer where T : new()
    {
        public GenericSerializer4() : base(typeof(T)) { }
    }

}