using System;
using System.Configuration;
using System.Linq;
using NUnit.Framework;

/*
 * References:
 * http://blog.danskingdom.com/adding-and-accessing-custom-sections-in-your-c-app-config/
 * http://www.codeproject.com/Articles/32490/Custom-Configuration-Sections-for-Lazy-Coders
 * 
 * */

namespace ConfigurationTests
{
    public class CommandsComplexSection : ConfigurationSection
    {
        public const string SectionName = "CommandsSection";
        private const string COMMANDS_COLLECTION_NAME = "Commands";

        [ConfigurationProperty(COMMANDS_COLLECTION_NAME)]
        [ConfigurationCollection(typeof(CommandConfigurationCollection), AddItemName = "command")]
        public CommandConfigurationCollection Commands { get { return (CommandConfigurationCollection)base[COMMANDS_COLLECTION_NAME]; } }
    }

    public class CommandConfigurationCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CommandConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CommandConfigurationElement)element).Name;
        }
    }

    public class CommandConfigurationElement: ConfigurationElement
    {
        [ConfigurationProperty("Name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["Name"]; }
            set { this["Name"] = value; }
        }

        [ConfigurationProperty("Comment")]
        public CommentElement Comment
        {
            get { return (CommentElement)this["Comment"]; }
            set { this["Comment"] = value; }
        }
    }
    public class CommentElement : ConfigurationElement
    {
        [ConfigurationProperty("Text", IsRequired = true)]
        public String Text
        {
            get
            {
                return (String)this["Text"];
            }
            set
            {
                this["Text"] = value;
            }
        }   
    }

    [TestFixture]
    public class CustomConfigComplexSectionTests
    {
        [Test]
        public void ExistsSection_command_True()
        {
            CommandsComplexSection sectionCommands = ConfigurationManager.GetSection(CommandsComplexSection.SectionName) as CommandsComplexSection;
            Assert.That(sectionCommands, Is.Not.Null);
            CommandConfigurationElement[] elements = new CommandConfigurationElement[sectionCommands.Commands.Count];
            sectionCommands.Commands.CopyTo(elements, 0);
            Assert.That(elements.First(c => c.Name == "command1").Name, Is.EqualTo("command1"));
            Assert.That(elements.First(c => c.Name == "command2").Name, Is.EqualTo("command2"));
            Assert.Throws<InvalidOperationException>(()=>elements.First(c => c.Name == "command" + Guid.NewGuid()));
        }
        [Test]
        public void NotExistsSection_command_False()
        {
            CommandsComplexSection sectionCommands = ConfigurationManager.GetSection("commandComplex"+Guid.NewGuid()) as CommandsComplexSection;
            Assert.That(sectionCommands, Is.Null);
        }

    }
}