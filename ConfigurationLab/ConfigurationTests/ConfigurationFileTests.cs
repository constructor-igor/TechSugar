using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using NUnit.Framework;

namespace ConfigurationTests
{
    [TestFixture]
    public class ConfigurationFileTests
    {
        private string m_configurationFileName = @"d:\@temp\test.xml";

        public class MyConfigurationSubClass
        {
            public string Title { get; set; }
        }
        public enum MySexEnum { Man, Female }
        [XmlRoot(ElementName = "Config")]
        public class TestConfigurationClass : IConfiguration
        {
            public string Name { get; set; }

            public bool Enabled { get; set; }

            public MySexEnum Sex { get; set; }

            public MyConfigurationSubClass SubClass { get; set; }

            [XmlIgnore]
            public string IgnoreName { get; set; }

            public Dictionary<string, string> GetAllConfigValues()
            {
                Dictionary<string, string> allKeys = new Dictionary<string, string> { ["Name"] = Name };
                return allKeys;
            }

            [XmlIgnore]
            public string ConfigPath { get; }

            public TestConfigurationClass()
            {
                SubClass = new MyConfigurationSubClass {Title = "title"};
            }
        }

        [Test]
        public void Test()
        {
            TestConfigurationClass configuration = new TestConfigurationClass
            {
                Name = "name",
                Sex = MySexEnum.Man,
                IgnoreName = "ignoreName"
            };
            ConfigManager.SaveConfiguration<TestConfigurationClass>(configuration, m_configurationFileName);
            TestConfigurationClass loadedConfig = ConfigManager.LoadConfiguration<TestConfigurationClass>(m_configurationFileName);
            Assert.That(loadedConfig.Name, Is.EqualTo("name"));
            Assert.That(loadedConfig.IgnoreName, Is.Null);
            Assert.Pass();
        }
        [Test]
        public void TestAppConfig()
        {
            TestConfigurationClass configuration = new TestConfigurationClass {Name = "name", Sex = MySexEnum.Female};
            ConfigManager.SaveConfiguration<TestConfigurationClass>(configuration, m_configurationFileName);
            TestConfigurationClass loadedConfig = ConfigManager.LoadConfiguration<TestConfigurationClass>(m_configurationFileName);
            Assert.That(loadedConfig.Name, Is.EqualTo("name"));
            Assert.Pass();
        }

        [Test]
        public void GetPropertiesList()
        {
            TestConfigurationClass configuration = new TestConfigurationClass { Name = "name", Sex = MySexEnum.Female};
            List<MyConfigurationProperty> configurationProperties = MyConfigManager.GetConfigurationProperties(configuration);

            foreach (MyConfigurationProperty property in configurationProperties)
            {
                Console.WriteLine("Property: {0}={1} ({2})", property.Name, property.Value, property.Type);
            }

            Assert.That(configurationProperties.Count, Is.EqualTo(4));            
        }

//        [Test]
//        public void SetPropertiesList()
//        {
//            TestConfigurationClass configuration = new TestConfigurationClass { Name = "name", Sex = MySexEnum.Female };
//            List<ConfigurationProperty> configurationProperties = MyConfigManager.GetConfigurationProperties(configuration);
//
//            foreach (ConfigurationProperty property in configurationProperties)
//            {
//                Console.WriteLine("Property: {0}={1} ({2})", property.Name, property.DefaultValue, property.Type);
//            }
//
//            configurationProperties[0]. DefaultValue = "new value";
//
//            Assert.That(configurationProperties.Count, Is.EqualTo(4));
//        }
    }

    public class MyConfigurationProperty
    {
        public readonly string Name;
        public readonly Type Type;
        public readonly object Value;

        public MyConfigurationProperty(string name, Type type, object value)
        {
            Name = name;
            Type = type;
            Value = value;
        }
    }

    public class MyAppConfiguration
    {
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

    public class MyGenericSerializer<T> : XmlSerializer
    {
        public MyGenericSerializer() : base(typeof(T))
        {
        }
    }

    public class MyConfigManager
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

        public static List<MyConfigurationProperty> GetConfigurationProperties(IConfiguration configuration)
        {
            return GetConfigurationProperties(string.Empty, configuration);
        }

        private static List<MyConfigurationProperty> GetConfigurationProperties(string parentName, object configuration)
        {
            if (!string.IsNullOrEmpty(parentName))
                parentName = parentName + ".";
            List<PropertyInfo> enabledXmlProperty = configuration.GetType().GetProperties()
                .Where(prop => !Attribute.IsDefined(prop, typeof(XmlIgnoreAttribute)))
                .ToList();

            List<MyConfigurationProperty> configurationProperties = enabledXmlProperty
                .Where(IsSimple)
                .Select(prop => new MyConfigurationProperty(parentName + prop.Name, prop.PropertyType, prop.GetValue(configuration, null))).ToList();
            configurationProperties.AddRange(enabledXmlProperty.Where(IsSubConfiguration).SelectMany(prop =>
            {
                object subObject = prop.GetValue(configuration, null);
                return GetConfigurationProperties(prop.Name, subObject);
            }));
            return configurationProperties;
        }

        private static bool IsSimple(PropertyInfo prop)
        {
            return prop.PropertyType.IsPrimitive || prop.PropertyType.IsValueType || prop.PropertyType.IsEnum || prop.PropertyType.IsSerializable;
        }

        private static bool IsSubConfiguration(PropertyInfo prop)
        {
            return !IsSimple(prop) && !prop.PropertyType.FullName.StartsWith("System.");
        }
    }
}