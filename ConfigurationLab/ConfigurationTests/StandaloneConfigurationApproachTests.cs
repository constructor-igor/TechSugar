using System;
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
                Name = "name",
                ConfigPath = configurationFile
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
                Address = "NY",
                ConfigPath = configurationFile
            };
            ConfigManager.SaveConfiguration<ConfigurationContainer2>(configuration, configurationFile);
            ConfigurationContainer2 loadedConfig = ConfigManager.LoadConfiguration<ConfigurationContainer2>(configurationFile);
            Assert.That(loadedConfig.Name, Is.EqualTo("name"));
            Assert.That(loadedConfig.Address, Is.EqualTo("NY"));
            Assert.Pass();
        }

        [Test]
        public void WriteAllConfigurations()
        {
            List<IConfiguration> allConfigurations = new List<IConfiguration>
            {
                new ConfigurationContainer1 {ConfigPath = Path.Combine(m_location, "test1.xml"), Name = "name1"},
                new ConfigurationContainer2 {ConfigPath = Path.Combine(m_location, "test2.xml"), Name = "name2", Address = "NY2"},
            };

            foreach (IConfiguration configuration in allConfigurations)
            {
                ConfigManager.SaveConfiguration(configuration, configuration.ConfigPath);
            }
        }

        [Test]
        public void LoadAllConfigurations()
        {
            List<IConfiguration> allConfigurations = new List<IConfiguration>
            {
                ConfigManager.LoadConfiguration<ConfigurationContainer1>(Path.Combine(m_location, "test1.xml")),
                ConfigManager.LoadConfiguration<ConfigurationContainer2>(Path.Combine(m_location, "test2.xml")),
            };

            foreach (IConfiguration configuration in allConfigurations)
            {
                Console.WriteLine("configuration path: {0}", configuration.ConfigPath);
            }
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
        public static void SaveConfiguration(IConfiguration configuration, string filePath)
        {
            var d1 = typeof(GenericSerializer<>);
            Type[] typeArgs = { configuration.GetType() };
            var makeme = d1.MakeGenericType(typeArgs);
            XmlSerializer serializer = (XmlSerializer) Activator.CreateInstance(makeme);
            using (TextWriter textWriter = File.CreateText(filePath))
            {
                serializer.Serialize(textWriter, configuration);
            }
        }
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
        string ConfigPath { get; }
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

        private string m_configPath;

        #region Implementation of IConfiguration
        [XmlIgnore]
        public string ConfigPath
        {
            get { return m_configPath; }
            set { m_configPath = value; }
        }
        #endregion
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

        private string m_configPath;

        #region Implementation of IConfiguration
        [XmlIgnore]
        public string ConfigPath
        {
            get { return m_configPath; }
            set { m_configPath = value; }
        }
        #endregion
    }

}