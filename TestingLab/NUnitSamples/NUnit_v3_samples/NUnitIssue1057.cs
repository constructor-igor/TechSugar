using System;
using NUnit.Framework;

namespace NUnit_v3_samples
{
    [TestFixture]
    public class NUnitIssue1057
    {
        [Test]
        public void OriginalTest()
        {
            //var address = "https://gist.githubusercontent.com/lbergnehr/56c355d41454855c25a4/raw/f4b95565a2330b695021c3d4b4fb55ff40aa4923/nunit_string_contains_test";
            //var result = new System.Net.WebClient().DownloadString(address);
            //Assert.That(result, Does.Contain(Guid.NewGuid().ToString()));
        }
        [Test]
        public void Test()
        {
            //var address = "https://gist.githubusercontent.com/lbergnehr/56c355d41454855c25a4/raw/f4b95565a2330b695021c3d4b4fb55ff40aa4923/nunit_string_contains_test";
            string result = Guid.NewGuid().ToString();
            Assert.That(result, Does.Contain(Guid.NewGuid().ToString()));
        }
    }
}