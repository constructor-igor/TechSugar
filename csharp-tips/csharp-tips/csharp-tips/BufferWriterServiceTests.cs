using System;
using System.IO;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class BufferWriterServiceTests
    {
        [Test, Explicit]
        public void WriteBuffer()
        {
            BufferWriterService.Instance.WriteBuffer(new byte[] {0, 1, 2, 3});
            BufferWriterService.Instance.WriteBuffer(new byte[] {0, 1, 2, 3});
            BufferWriterService.Instance.WriteBuffer(new byte[] {0, 1, 2, 3, 4, 5, 6});
        }
    }

    public class BufferWriterService
    {
        private static readonly object m_lockObject = new object();
        private static readonly BufferWriterService m_instance = new BufferWriterService();
        private static int m_counter;
        public static string TargetFolder;

        public static BufferWriterService Instance
        {
            get { return m_instance; }
        }

        private BufferWriterService()
        {
            m_counter = 0;
            TargetFolder = String.Format(@"d:\@temp\DB\Session_{0:HH_mm_ss}_{1}", DateTime.Now, Guid.NewGuid());
            Directory.CreateDirectory(TargetFolder);
        }

        public string WriteBuffer(byte[] buffer)
        {
            lock (m_lockObject)
            {
                string targetFileName = Path.Combine(TargetFolder, String.Format("buffer_{0:00000}.dat", m_counter++));
                File.WriteAllBytes(targetFileName, buffer);
                return targetFileName;
            }
        }

    }
}