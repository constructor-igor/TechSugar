using System.Collections.Generic;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class EnumTests
    {
        public enum Options
        {
            Option1,
            Option2,
            Option3
        };

        readonly Dictionary<Options, string> m_userNames = new Dictionary<Options, string>
            {
                {Options.Option1, "First"},
                {Options.Option2, "Second"},
                {Options.Option3, "Third"}
            };

        [Test]
        public void ListOfEnumBasedData()
        {
            Assert.That(m_userNames[Options.Option1], Is.EqualTo("First"));
        }
    }
}