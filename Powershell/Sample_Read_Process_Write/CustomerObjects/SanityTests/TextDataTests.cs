using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using Customer.DataProcessing;
using Customer.TextData;
using NUnit.Framework;

namespace SanityTests
{
    [TestFixture]
    public class TextDataTests
    {
        [Test]
        public void Test()
        {
            var textData = new CustomerTextData("Black and White\nBlack");
            int countOfBlacks = textData.GetNumberOf("Black");
            Assert.AreEqual(2, countOfBlacks);
        }
    }
}
