using NUnit.Framework;

namespace csharp_tips
{
    [TestFixture]
    public class ReferenceTests
    {
        [Test]
        public void Test()
        {
            object objectA = new object();

            function1(objectA);
            Assert.That(objectA, Is.Not.Null);

            function2(ref objectA);
            Assert.That(objectA, Is.Null);
        }

        void function1(object objectX)
        {
            objectX = null;
        }
        void function2(ref object objectX)
        {
            objectX = null;
        }
    }
}