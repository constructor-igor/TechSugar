using System.Text;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class StringSamples
    {
        [Test]
        public void EmptyString_StringBuilderAppend()
        {
            var tests = new StringBuilder();
            var testFilter = new StringBuilder("<filter>");

            testFilter.Append(tests.ToString());

            Assert.That(testFilter.ToString(), Is.EqualTo("<filter>"));
        }
    }
}