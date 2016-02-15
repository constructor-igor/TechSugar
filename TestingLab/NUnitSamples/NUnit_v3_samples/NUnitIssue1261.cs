using NUnit.Framework;
using NUnit.Framework.Internal;

namespace NUnit_v3_samples
{
    [TestFixture]
    public class NUnitIssue1261
    {
        public class Class1<T, S>
        {
            public class Class2<K>
            {
                public void T()
                { }
            }
        }

        [Test]
        public void GetDisplayName()
        {
            var s = new Class1<int, string>.Class2<long>();

            string actual = TypeHelper.GetDisplayName(s.GetType());
            Assert.That(actual, Is.EqualTo("NUnitIssue1261+Class1<Int32,String>+Class2<Int64>"));
        }
    }
}