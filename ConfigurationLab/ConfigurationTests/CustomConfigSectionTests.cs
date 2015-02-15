using System;
using System.Collections.Specialized;
using System.Configuration;
using NUnit.Framework;

/*
 * References:
 * http://blog.danskingdom.com/adding-and-accessing-custom-sections-in-your-c-app-config/
 * http://www.codeproject.com/Articles/32490/Custom-Configuration-Sections-for-Lazy-Coders
 * 
 * */

namespace ConfigurationTests
{
    [TestFixture]
    public class CustomConfigSectionTests
    {
        [Test]
        public void ExistsSection_command_True()
        {
            NameValueCollection sectionCommand = ConfigurationManager.GetSection("commandSimple") as NameValueCollection;
            Assert.That(sectionCommand["key11"], Is.EqualTo("value11"));
            Assert.That(sectionCommand["key12"], Is.EqualTo("value12"));
        }
        [Test]
        public void NotExistsSection_command_False()
        {
            NameValueCollection sectionCommand = ConfigurationManager.GetSection("commandSimple"+Guid.NewGuid()) as NameValueCollection;
            Assert.That(sectionCommand, Is.Null);
        }
    }
}
