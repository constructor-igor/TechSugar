using System;
using NUnit.Framework;

namespace csharp_tips
{
    public interface ICloneable<T> : ICloneable
        where T : ICloneable<T>
    {
        new T Clone();
    }
    public class MyData: ICloneable, ICloneable<MyData>
    {
        public string Name { get; set; }

        #region Implementation of ICloneable

        MyData ICloneable<MyData>.Clone()
        {
            return new MyData { Name = Name };
        }

        public object Clone()
        {
            return new MyData() {Name = Name};
        }
        #endregion
    }
    [TestFixture]
    public class CloneTests
    {
        [Test]
        public void Clone()
        {
            MyData data = new MyData {Name = "Joe"};
            MyData cloned = (MyData) data.Clone();
            Assert.That(cloned.Name, Is.EqualTo(data.Name));
        }
        [Test]
        public void Clone_Generic()
        {
            MyData data = new MyData { Name = "Joe" };
            MyData cloned = (MyData) data.Clone();
            Assert.That(cloned.Name, Is.EqualTo(data.Name));
        }
    }
}