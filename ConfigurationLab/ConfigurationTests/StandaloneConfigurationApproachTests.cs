using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using NUnit.Framework;

namespace ConfigurationTests
{
    [TestFixture]
    public class StandaloneConfigurationApproachTests
    {
        private string m_location;
        [SetUp]
        public void SetUp()
        {
            m_location = Path.GetDirectoryName(typeof(ConfigurationContainer1).Assembly.Location);
        }
        [Test]
        public void Test1()
        {            
            string configurationFile = Path.Combine(m_location, "test1.xml");

            ConfigurationContainer1 configuration = new ConfigurationContainer1
            {
                Name = "name"
            };
            ConfigManager.SaveConfiguration<ConfigurationContainer1>(configuration, configurationFile);
            ConfigurationContainer1 loadedConfig = ConfigManager.LoadConfiguration<ConfigurationContainer1>(configurationFile);
            Assert.That(loadedConfig.Name, Is.EqualTo("name"));
            Assert.Pass();
        }
        [Test]
        public void Test2()
        {
            string configurationFile = Path.Combine(m_location, "test2.xml");

            ConfigurationContainer2 configuration = new ConfigurationContainer2
            {
                Name = "name",
                Address = "NY"
            };
            ConfigManager.SaveConfiguration<ConfigurationContainer2>(configuration, configurationFile);
            ConfigurationContainer2 loadedConfig = ConfigManager.LoadConfiguration<ConfigurationContainer2>(configurationFile);
            Assert.That(loadedConfig.Name, Is.EqualTo("name"));
            Assert.That(loadedConfig.Address, Is.EqualTo("NY"));
            Assert.Pass();
        }
    }

    public class GenericSerializer<T> : XmlSerializer
    {
        public GenericSerializer() : base(typeof(T))
        {
        }
    }
    public class ConfigManager
    {
        public static void SaveConfiguration<P>(IConfiguration configuration, string filePath)
        {
            var serializer = new GenericSerializer<P>();
            using (TextWriter textWriter = File.CreateText(filePath))
            {
                serializer.Serialize(textWriter, configuration);
            }
        }
        public static P LoadConfiguration<P>(string filePath)
        {
            var serializer = new GenericSerializer<P>();
            using (TextReader textReader = File.OpenText(filePath))
            {
                P loadedConfig = (P)serializer.Deserialize(textReader);
                return loadedConfig;
            }
        }
    }

    public interface IConfiguration
    {
        
    }

    [XmlRoot(ElementName = "MyConfig1")]
    public class ConfigurationContainer1 : IConfiguration
    {
        public string Name { get; set; }

        public Dictionary<string, string> GetAllConfigValues()
        {
            Dictionary<string, string> allKeys = new Dictionary<string, string> { ["Name"] = Name };
            return allKeys;
        }

        public string ConfigPath { get; }
    }

    [XmlRoot(ElementName = "MyConfig2")]
    public class ConfigurationContainer2 : IConfiguration
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public Dictionary<string, string> GetAllConfigValues()
        {
            Dictionary<string, string> allKeys = new Dictionary<string, string> { ["Name"] = Name, ["Address"] = Address};
            return allKeys;
        }

        public string ConfigPath { get; }
    }

}