using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class EnumerationSamples
    {
        [Test]
        public void ResharperWarning()
        {
            MyDirectory myDirectory = new MyDirectory();
            IEnumerable<DataFile> allFiles = myDirectory
                .GetFiles(@"d:\")
                .Select(fileName => new DataFile(fileName));
            IEnumerable<DataFile> taskFiles = allFiles
                .Where(file => !file.FileName.EndsWith("exe"));
            taskFiles.ForEach(file=> { Console.WriteLine("[1] file: {0}", file.FileName); });
            taskFiles.ForEach(file=> { Console.WriteLine("[2] file: {0}", file); });
        }

        internal class MyDirectory
        {
            internal string[] GetFiles(string folder)
            {
                Console.WriteLine("GetFiles(\"{0}\")", folder);
                return Directory.GetFiles(folder);
            }
        }

        internal class DataFile
        {
            private readonly string m_fileName;

            internal string FileName
            {
                get
                {
                    Console.WriteLine("DataFile.FileName_get(): {0}", m_fileName);
                    return m_fileName;
                }
            }
            internal DataFile(string fileName)
            {
                m_fileName = fileName;
                Console.WriteLine("DataFile.ctor(): {0}", m_fileName);
            }
        }
    }

    internal static class EnumeratorExtensions
    {
        internal static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (T item in list)
            {
                action(item);
            }
        }
    }
}