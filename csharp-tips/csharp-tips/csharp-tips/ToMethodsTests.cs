using System.Collections.Specialized;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using NUnit.Framework;

/*
 * 
 * http://stackoverflow.com/questions/16705124/clipboard-task-error
 * 
 * */

namespace csharp_tips
{
    [TestFixture]
    public class ToMethodsTests
    {
        [Test]
        public void FileToClipboard()
        {
            string testDataFilePath = @"..\..\Data\test.txt";
            StringCollection paths = new StringCollection { testDataFilePath };
            Assert.That(File.Exists(testDataFilePath));
            Assert.That(() => Clipboard.SetFileDropList(paths), Throws.TypeOf<ThreadStateException>());
        }
    }
}