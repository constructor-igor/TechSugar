using System.Collections.Concurrent;
using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class ConcurrentDictionaryTests
    {
        [Test]
        public void Test()
        {
            ConcurrentDictionary<string, Container> dictionary = new ConcurrentDictionary<string, Container>();
            string key = "one";
            Container container = new Container(key, "new");
            bool add = dictionary.TryAdd(key, container);
            Assert.That(dictionary.Count, Is.EqualTo(1));
            Assert.That(add, Is.True);

            add = dictionary.TryAdd(key, container);
            Assert.That(add, Is.False);

            Container foundContainer = dictionary[key];
            Assert.That(foundContainer.Id, Is.EqualTo(key));
            Assert.That(foundContainer.Status, Is.EqualTo("new"));
            Assert.That(dictionary.Count, Is.EqualTo(1));

            Container containerCompleted = new Container(key, "completed");
            dictionary[key] = containerCompleted;
            Container foundCompletedContainer = dictionary[key];
            Assert.That(foundCompletedContainer.Id, Is.EqualTo(key));
            Assert.That(foundCompletedContainer.Status, Is.EqualTo("completed"));
            Assert.That(dictionary.Count, Is.EqualTo(1));
        }
    }

    public class Container
    {
        public readonly string Id;
        public readonly string Status;

        public Container(string id, string status)
        {
            Id = id;
            Status = status;
        }
    }
}