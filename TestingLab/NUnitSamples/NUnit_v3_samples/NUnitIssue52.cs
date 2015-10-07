using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace NUnit_v3_samples
{
    [TestFixture]
    public class NUnitIssue52
    {
        class SelfContainer: IEnumerable
        {
            public IEnumerator GetEnumerator() { yield return this; }
        }
        [Test]
        public void SelfContainedItemFoundInArray()
        {
            var item = new SelfContainer();
            var items = new SelfContainer[] { new SelfContainer(), item };

            // work around
            Assert.True(((ICollection<SelfContainer>)items).Contains(item));

            // causes StackOverflowException
            //Assert.Contains(item, items);
            //Assert.That(item, Is.SubsetOf(items));
            Assert.That(items, Does.Contain(item));
            Console.WriteLine("test completed");
        }
    }
}