using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class JsonTests
    {
        public class Data
        {
            public readonly string Name;
            public readonly Dictionary<string, string> Parameters;

            public Data(string name, Dictionary<string, string> parameters)
            {
                Name = name;
                Parameters = parameters;
            }
        }
        [Test]
        public void WriteReadJson()
        {
            Data data = new Data("name", new Dictionary<string, string> { { "parameter1", "value1" }, { "parameter2", "value2" } });
            String jsonText = JObject.FromObject(data).ToString();
            Data actualData = JsonConvert.DeserializeObject<Data>(jsonText);

            Assert.That(actualData.Name, Is.EqualTo("name"));
            Assert.That(actualData.Parameters, Is.EquivalentTo(new Dictionary<string, string> { { "parameter1", "value1" }, { "parameter2", "value2" } }));
        }
    }
}