using System;
using NUnit.Framework;

/*
 * The test project created for oz-code review (include checking different ideas for Assert visualization)
 * Part of the ideas can be found in http://ozcode.userecho.com/
 * */

namespace AssertSamples
{
    [TestFixture]
    public class AssertSamplesOfSimpleTypes
    {
        [Test]
        [ExpectedException]
        public void AssertSimpleType_String()
        {
            const string expectedResult = "test_5";
            string actualResults = Foo(4);
            Assert.AreEqual(expectedResult, actualResults);

            Console.WriteLine("test");
        }

        [Test]
        [ExpectedException]
        public void AssertSimpleType_Double()
        {
            const double expectedResult = 0.03;
            double actualResults = Foo(0.04);
            Assert.AreEqual(expectedResult, actualResults, 0.001);

            Console.WriteLine("test");
        }

        [Test]
        [ExpectedException]
        public void AssertSimpleType_Int()
        {
            const int expectedResult = 30;
            int actualResults = FooInt2Int(40);
            Assert.AreEqual(expectedResult, actualResults, 5);

            Console.WriteLine("test");
        }

        [Test]
        [ExpectedException]
        public void AssertSimpleTypes_Multi()
        {
            const string expectedResult = "test_5";
            const int expectedId = 15;

            Tuple<string, int> actualData = Foo(4, 15);

            string actualResult = actualData.Item1;
            int actualId = actualData.Item2;

            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(expectedId, actualId);
        }

        #region foo methods
        private int FooInt2Int(int fooResult)
        {
            return fooResult;
        }
        private double Foo(double fooResult)
        {
            return fooResult;
        }
        private string Foo(int fooId)
        {
            return string.Format("test_{0}", fooId);
        }
        private Tuple<string, int> Foo(int fooId, int fooResult)
        {
            return new Tuple<string, int>(string.Format("test_{0}", fooId), fooResult);            
        }

        #endregion
    }
}
