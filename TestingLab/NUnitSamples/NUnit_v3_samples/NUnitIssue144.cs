using System.Collections;
using NUnit.Framework;

namespace NUnit_v3_samples
{
    [TestFixture]
    public class NUnitIssue144
    {
        [Test]
        public void GeneratedErrorMessage()
        {
            int actual = 10;
            int expected = 15;

            // Assert
            var comparer = new MyEqualityComparer();
            Assert.That(actual, Is.EqualTo(expected).Using(comparer));
        }

        [Test]
        public void CustomMessage()
        {
            int actual = 10;
            int expected = 15;

            // Assert
            var comparer = new MyEqualityComparer();
            Assert.That(actual, Is.EqualTo(expected).Using(comparer), "custom error message (expected={0}, actual={1}", expected, actual);
        }

        [Test]
        public void CustomLazyMessage()
        {
            int actual = 10;
            int expected = 15;

            // Assert
            var comparer = new MyEqualityComparer();
            Assert.That(actual, Is.EqualTo(expected).Using(comparer));
        }
    }

    public class MyEqualityComparer : IEqualityComparer
    {
        #region Implementation of IEqualityComparer

        public bool Equals(object x, object y)
        {
            int intX = (int) x;
            int intY = (int) y;
            return intX == intY;
        }
        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
        #endregion
    }
}