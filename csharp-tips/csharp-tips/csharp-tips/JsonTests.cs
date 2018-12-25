using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class JsonTests
    {
        [DataContract]
        public class Person
        {
            // excluded from serialization
            // does not have DataMemberAttribute
            public Guid Id { get; set; }
            [DataMember]
            public string Name { get; set; }

            public Person()
            {
                Id = Guid.NewGuid();
            }

            #region Overrides of Object
            public override bool Equals(object obj)
            {
                Person otherPerson = obj as Person;
                return Name == otherPerson.Name;
            }
            #endregion
        }

        [DataContract]
        public class Student : Person
        {
            [DataMember] public string Score;
            #region Overrides of Object
            public override bool Equals(object obj)
            {
                Student other = obj as Student;
                if (other == null)
                    return false;
                return base.Equals(obj) && Score == other.Score;
            }
            #endregion
        }

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
            string jsonText = JObject.FromObject(data).ToString();
            Data actualData = JsonConvert.DeserializeObject<Data>(jsonText);

            Assert.That(actualData.Name, Is.EqualTo("name"));
            Assert.That(actualData.Parameters, Is.EquivalentTo(new Dictionary<string, string> { { "parameter1", "value1" }, { "parameter2", "value2" } }));
        }

        [Test]
        public void PersonJson()
        {
            Person person = new Person {Name = "Joe"};
            string personAsJson = JsonConvert.SerializeObject(person, Formatting.Indented);
            Console.WriteLine(personAsJson);
            Person actualPerson = JsonConvert.DeserializeObject<Person>(personAsJson);
            Assert.That(actualPerson, Is.EqualTo(person));
        }
        [Test]
        public void PersonArraysJson()
        {
            Person[] persons = { new Person { Name = "Joe" }, new Person {Name = "Tom"}};
            string personsAsJson = JsonConvert.SerializeObject(persons, Formatting.Indented);
            Console.WriteLine(personsAsJson);
            Person[] actualPersons = JsonConvert.DeserializeObject<Person[]>(personsAsJson);
            Assert.That(actualPersons, Is.EqualTo(persons));
        }
        [Test]
        public void StudentJson()
        {
            Student student = new Student { Name = "Joe", Score = "100"};
            string studentAsJson = JsonConvert.SerializeObject(student, Formatting.Indented);
            Console.WriteLine(studentAsJson);
            Student actualStudent = JsonConvert.DeserializeObject<Student>(studentAsJson);
            Assert.That(actualStudent, Is.EqualTo(student));
        }
        [Test]
        public void WriteStudentReadPersonJson()
        {
            Student student = new Student { Name = "Joe", Score = "100" };
            string studentAsJson = JsonConvert.SerializeObject(student, Formatting.Indented);
            Console.WriteLine(studentAsJson);
            Person actual = JsonConvert.DeserializeObject<Person>(studentAsJson);
            Assert.IsInstanceOf<Person>(actual);
            Assert.IsNotInstanceOf<Student>(actual);
        }
        [Test]
        public void WritePersonReadStudentJson()
        {
            Person person = new Person { Name = "Joe" };
            string personAsJson = JsonConvert.SerializeObject(person, Formatting.Indented);
            Console.WriteLine(personAsJson);
            Student actual = JsonConvert.DeserializeObject<Student>(personAsJson);
            Assert.IsInstanceOf<Person>(actual);
            Assert.IsInstanceOf<Student>(actual);
        }
    }
}