using NUnit.Framework;

namespace NUnit_v3_samples
{
    [TestFixture]
    public class NUnitCollectionTests
    {
        [Test]
        public void Test()
        {
            byte[,] actual = {
                {10, 20, 30, 20},
                {11, 21, 31, 41}
            };
            byte[,] expected = {
                {10, 20, 30, 20},
                {11, 21, 31, 41}
            };

            Assert.That(actual, Is.EquivalentTo(expected));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Assert_AreEqual_1024()
        {
            byte[,] actual = new byte[1024, 1024];
            byte[,] expected = new byte[1024, 1024];
            
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void CollectionAssert_AreEqual_1024()
        {
            byte[,] actual = new byte[1024, 1024];
            byte[,] expected = new byte[1024, 1024];

            CollectionAssert.AreEqual(expected, actual);
        }
        [Test]
        public void Assert_That_EquivalentTo_1024()
        {
            byte[,] actual = new byte[1024, 1024];
            actual[10, 10] = 10;
            actual[20, 20] = 20;
            byte[,] expected = new byte[1024, 1024];
            expected[10, 10] = 10;
            expected[20, 20] = 20;

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}