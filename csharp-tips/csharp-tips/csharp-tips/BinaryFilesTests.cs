using System.IO;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class BinaryFilesTests
    {
        [Test]
        public void Sample_File()
        {
            byte[] data = {0, 1, 2, 3, 4};
            string tempFile = Path.GetTempFileName();
            try
            {
                File.WriteAllBytes(tempFile, data);
                byte[] actualData = File.ReadAllBytes(tempFile);
                Assert.That(actualData, Is.EquivalentTo(data));
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

        public class MyData
        {
            public int IntData;
            public string StrData;
        }
        [Test]
        public void Sample_Stream()
        {
            MyData data = new MyData {IntData = 10, StrData = "MyStrData"};
            string tempFile = Path.GetTempFileName();
            try
            {
                using (BinaryWriter bw = new BinaryWriter(new FileStream(tempFile, FileMode.Create)))
                {
                    bw.Write(data.IntData);
                    bw.Write(data.StrData);
                }

                MyData actualData = new MyData();
                using (BinaryReader br = new BinaryReader(new FileStream(tempFile, FileMode.Open)))
                {
                    actualData.IntData = br.ReadInt32();
                    actualData.StrData = br.ReadString();
                }

                Assert.That(actualData.IntData, Is.EqualTo(data.IntData));
                Assert.That(actualData.StrData, Is.EqualTo(data.StrData));
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

    }
}