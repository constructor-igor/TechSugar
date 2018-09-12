using System;
using System.IO;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class FinalizerTests
    {
        [Test]
        public void Test_using()
        {
            string fileName;
            using (MyTemporaryFile file = new MyTemporaryFile())
            {
                fileName = file.FileName;
                Assert.That(File.Exists(fileName), Is.True);
            }
            Assert.That(File.Exists(fileName), Is.False);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        [Test]
        public void Test_Dispose()
        {
            MyTemporaryFile file = new MyTemporaryFile();
            string fileName = file.FileName;
            Assert.That(File.Exists(fileName), Is.True);
            file.Dispose();
            Assert.That(File.Exists(fileName), Is.False);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Assert.That(File.Exists(fileName), Is.False);
        }
        [Test]
        public void Test_NoDispose()
        {
            var fileName = Foo();
            Assert.That(File.Exists(fileName), Is.True);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Assert.That(File.Exists(fileName), Is.False);
        }

        string Foo()
        {
            MyTemporaryFile file = new MyTemporaryFile();
            var fileName = file.FileName;
            return fileName;
        }
    }

    public class MyTemporaryFile: IDisposable
    {
        public readonly string FileName;
        public MyTemporaryFile()
        {
            Console.WriteLine("MyTemporaryFile.ctor");
            FileName = Path.GetTempFileName();
        }

        #region IDisposable
        public void Dispose()
        {
            Console.WriteLine("MyTemporaryFile.Dispose");
            File.Delete(FileName);
            GC.SuppressFinalize(this);
        }
        #endregion
        ~MyTemporaryFile()
        {
            Console.WriteLine("MyTemporaryFile.Finalizer");
            File.Delete(FileName);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }
            // Cleanup unmanaged resource
            File.Delete(FileName);
        }
    }
}
