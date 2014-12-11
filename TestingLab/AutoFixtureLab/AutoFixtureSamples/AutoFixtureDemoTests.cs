using NUnit.Framework;
using Ploeh.AutoFixture;

namespace AutoFixtureSamples
{
    //
    //
    //  References:
    //  - https://github.com/AutoFixture/AutoFixture/wiki/Cheat-Sheet
    //

    [TestFixture]
    public class AutoFixtureDemoTests
    {
        [Test]
        [Repeat(100)]
        public void IntroductoryTest()
        {
            // Fixture setup
            var fixture = new Fixture();

            var expectedNumber = fixture.Create<int>();
            var sut = fixture.Create<MyClass>();
            // Exercise system
            int result = sut.Echo(expectedNumber);            
            // Verify outcome
            Assert.AreEqual(expectedNumber, result, "Echo");

            var x = fixture.Create<double>();
            var y = fixture.Create<double>();
            double sum = sut.Add(x, y);
            Assert.AreEqual(sum, x+y);

            // Teardown
        }
    }

    public class MyClass
    {
        public string name;
        public object data;
        public MySubClass SubClass;
        public int Echo(int value)
        {
            return value;
        }

        public double Add(double x, double y)
        {
            return x + y;
        }
    }

    public class MySubClass
    {
        public string name;
        private int id;

        public MySubClass(int id)
        {
            this.id = id;
        }
    }
}
