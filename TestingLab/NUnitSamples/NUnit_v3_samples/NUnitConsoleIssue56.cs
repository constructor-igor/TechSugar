using System.Text.RegularExpressions;
using NUnit.Framework;

namespace NUnit_v3_samples
{
    namespace Issue56
    {
        [TestFixture]
        public class Category1Test
        {
            [Test]
            public void category1_test_1()
            {
                Assert.Pass();
            }

            [Test]
            public void category1_test_2()
            {
                Assert.Pass();
            }
        }

        namespace NameSpace2
        {
            [TestFixture]
            public class TestSuite1Test
            {
                [Test]
                public void testsuite1_test_1()
                {
                    Assert.Pass();
                }

                [Test]
                public void testsuite1_test_2()
                {
                    Assert.Pass();
                }

                [Test]
                public void testsuite1_test_3()
                {
                    Assert.Pass();
                }
            }
        }
    }

    [TestFixture]
    public class RegexpIssue56Tests
    {
        [TestCase("NUnit_v3_samples.Issue56.Category1Test.category1_test_1", true)]
        [TestCase("NUnit_v3_samples.Issue56.Category1Test.category1_test_2", true)]
        [TestCase("NUnit_v3_samples.Issue56.NameSpace2.TestSuite1Test.testsuite1_test_1", false)]
        [TestCase("NUnit_v3_samples.Issue56.NameSpace2.TestSuite1Test.testsuite1_test_2", false)]
        [TestCase("NUnit_v3_samples.Issue56.NameSpace2.TestSuite1Test.testsuite1_test_3", false)]
        public void Test(string fullTestName, bool expected)
        {
            Regex regex = new Regex(@"NUnit_v3_samples.Issue56.\w+.\w+$");
            Match match = regex.Match(fullTestName);
            Assert.That(match.Success, Is.EqualTo(expected));
        }

        [Test]
        public void Investigation()
        {
            Regex regex = new Regex(@"NUnit_v3_samples.Issue56.\w+.\w+$");
            Match match = regex.Match("NUnit_v3_samples.Issue56.Category1Test.category1_test_1");
            Assert.That(match.Success, Is.True);
        }
    }
}